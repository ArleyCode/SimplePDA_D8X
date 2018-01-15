// 2DReadEVC40.cpp : Defines the entry point for the application.
//

//
// Imager Sample Program -- Single Decode(Trigger key) --
// File Name : IMGSingleDecode1.cpp
// Version   : 1.00.03
// Date      : 11/ 8/2004
// Copyright (C) 2004 CASIO COMPUTER CO.,LTD.
//
#include "StdAfx.h"
#include <commctrl.h>
#include "ImagerLib.h"

ATOM MyRegisterClass( HINSTANCE, LPTSTR);
BOOL InitInstance( HINSTANCE, int);
LRESULT CALLBACK WndProc( HWND, UINT, WPARAM, LPARAM);
BOOL 	DispSymbol(HWND, TCHAR);


HINSTANCE hInst;
HWND hwndCB;

int WINAPI WinMain( HINSTANCE hInstance, HINSTANCE hPrevInstance,
	LPTSTR lpCmdLine, int nCmdShow)
{
	MSG msg;

	if ( !InitInstance( hInstance, nCmdShow)) 
	{
		return FALSE;
	}

	while (GetMessage(&msg, NULL, 0, 0)) 
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return msg.wParam;
}

ATOM MyRegisterClass( HINSTANCE hInstance, LPTSTR szWindowClass)
{
	WNDCLASS wc;

    wc.style			= CS_HREDRAW | CS_VREDRAW;
    wc.lpfnWndProc		= ( WNDPROC)WndProc;
    wc.cbClsExtra		= 0;
    wc.cbWndExtra		= 0;
    wc.hInstance		= hInstance;
    wc.hIcon			= NULL;
    wc.hCursor			= 0;
    wc.hbrBackground	= ( HBRUSH)GetStockObject( WHITE_BRUSH);
    wc.lpszMenuName		= 0;
    wc.lpszClassName	= szWindowClass;

	return RegisterClass( &wc);
}

BOOL InitInstance( HINSTANCE hInstance, int nCmdShow)
{
	HWND hWnd;
	TCHAR szTitle[ 100] = L"Imager Sample";
	TCHAR szWindowClass[ 100] = L"ImagerSampleWindowClass";

	hInst = hInstance;

	MyRegisterClass( hInstance, szWindowClass);

	hWnd = CreateWindow( szWindowClass, szTitle, WS_VISIBLE,
		CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
		NULL, NULL, hInstance, NULL);

	if ( !hWnd)
	{	
		return FALSE;
	}

	ShowWindow( hWnd, nCmdShow);
	UpdateWindow( hWnd);
	if ( hwndCB)
	{
		CommandBar_Show( hwndCB, TRUE);
	}
	return TRUE;
}

#define	ID_HOTKEY_L 1			// Hotkey ID of L Trigger
#define	ID_HOTKEY_R 2			// Hotkey ID of R Trigger
#define	ID_HOTKEY_C 3			// Hotkey ID of Center Trigger
#define VKEY_TRIGGER_L VK_F24	// Virtual Key of L Trigger
#define VKEY_TRIGGER_R VK_F21	// Virtual Key of R Trigger
#define VKEY_TRIGGER_C VK_F20	// Virtual Key of Center Trigger

BOOL fTrigger( VOID);

LRESULT CALLBACK WndProc( HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	static HWND hEditMessage;
	static HWND hEditMessage2;
	HDC hdc;
	PAINTSTRUCT ps;
	TCHAR chCodeID, chAimID, chModifier, szDecodeMessage[ 512];
	DWORD dwLength;
	int nResult;
	BOOL bResPeek;
	MSG msg;
	// ini File default pass
	TCHAR pFileName[64]={TEXT("\\FlashDisk\\System Settings\\IMGSet.ini")};

	switch (message) 
	{
	case WM_KEYDOWN:
		switch( LOWORD( wParam))
		{
		case VK_ESCAPE:
		case VK_DELETE:
			PostMessage( hWnd, WM_DESTROY, 0, 0);
			break;
		}
		break;

	case WM_HOTKEY:
		switch( wParam)
		{
		case ID_HOTKEY_L:
		case ID_HOTKEY_R:
		case ID_HOTKEY_C:
			// read symbol
			nResult = IMGWaitForDecode( 5000, szDecodeMessage, &chCodeID, &chAimID,
				&chModifier, &dwLength, fTrigger);
			if ( nResult == IMG_SUCCESS)
			{
				// symbol data display
				SetWindowText( hEditMessage, szDecodeMessage);
				DispSymbol(hEditMessage2, chCodeID);
			}
			break;
		default:
			break;
		}
		do
		{
			bResPeek = PeekMessage(&msg , hWnd, WM_HOTKEY, WM_HOTKEY, PM_REMOVE);
		} while( bResPeek == TRUE);

		break;

	case WM_CREATE:
		hwndCB = CommandBar_Create(hInst, hWnd, 1);			
		CommandBar_InsertMenubar( hwndCB, hInst, IDM_MENU, 0);
		CommandBar_AddAdornments( hwndCB, 0, 0);

		hEditMessage = CreateWindow( TEXT( "edit"), NULL,
			WS_CHILD | WS_VISIBLE | WS_BORDER | ES_MULTILINE,
			8, 30, 160, 140, hWnd, ( HMENU)1, hInst, NULL);
		hEditMessage2 = CreateWindow( TEXT( "edit"), NULL,
			WS_CHILD | WS_VISIBLE | WS_BORDER | ES_MULTILINE,
			8, 180, 160, 20, hWnd, ( HMENU)2, hInst, NULL);
		RegisterHotKey( hWnd, ID_HOTKEY_L,  0, VKEY_TRIGGER_L);
		RegisterHotKey( hWnd, ID_HOTKEY_R,  0, VKEY_TRIGGER_R);
		RegisterHotKey( hWnd, ID_HOTKEY_C,  0, VKEY_TRIGGER_C);

		// IMGDRV open
		IMGInit();
		// ini File read default value set
		IMGLoadConfigFile(pFileName);
		// IMGDRV mode will be ini File vallue 
		IMGConnect();
		break;

	case WM_PAINT:
		hdc = BeginPaint( hWnd, &ps);
		EndPaint( hWnd, &ps);
		break;

	case WM_DESTROY:
		// IMGDRV Close
		IMGDisconnect();
		UnregisterHotKey( hWnd, ID_HOTKEY_L);
		UnregisterHotKey( hWnd, ID_HOTKEY_R);
		UnregisterHotKey( hWnd, ID_HOTKEY_C);
		IMGDeinit();
		CommandBar_Destroy( hwndCB);
		PostQuitMessage( 0);
		break;

	default:
		return DefWindowProc( hWnd, message, wParam, lParam);
	}
   return 0;
}

BOOL fTrigger( VOID)
{
	if( GetAsyncKeyState( VKEY_TRIGGER_L) < 0 || GetAsyncKeyState( VKEY_TRIGGER_R) || GetAsyncKeyState( VKEY_TRIGGER_C) < 0)
	{
		return TRUE;
	}
	else
	{
		return FALSE;
	}
}

// search symbol kind
BOOL DispSymbol(HWND hEditMessage2, TCHAR chCodeID)
{
	if (chCodeID == 'd')
	{
		SetWindowText( hEditMessage2, TEXT("EAN13"));
		return TRUE;
	}
	else if (chCodeID == 'A')
	{
		SetWindowText( hEditMessage2, TEXT("Australian Postal"));
		return TRUE;
	}
	else if (chCodeID == 'z')
	{
		SetWindowText( hEditMessage2, TEXT("Aztec"));
		return TRUE;
	}
	else if (chCodeID == 'B')
	{
		SetWindowText( hEditMessage2, TEXT("British Postal"));
		return TRUE;
	}
	else if (chCodeID == 'C')
	{
		SetWindowText( hEditMessage2, TEXT("Canadian Postal"));
		return TRUE;
	}
	else if (chCodeID == 'a')
	{
		SetWindowText( hEditMessage2, TEXT("Codabar"));
		return TRUE;
	}
	else if (chCodeID == 'q')
	{
		SetWindowText( hEditMessage2, TEXT("Codablock F"));
		return TRUE;
	}
	else if (chCodeID == 'h')
	{
		SetWindowText( hEditMessage2, TEXT("Code 11"));
		return TRUE;
	}
	else if (chCodeID == 'j')
	{
		SetWindowText( hEditMessage2, TEXT("Code 128 / ISBT"));
		return TRUE;
	}
	else if (chCodeID == 'b')
	{
		SetWindowText( hEditMessage2, TEXT("Code 39"));
		return TRUE;
	}
	else if (chCodeID == 'l')
	{
		SetWindowText( hEditMessage2, TEXT("Code 49"));
		return TRUE;
	}
	else if (chCodeID == 'i')
	{
		SetWindowText( hEditMessage2, TEXT("Code 93"));
		return TRUE;
	}
	else if (chCodeID == 'y')
	{
		SetWindowText( hEditMessage2, TEXT("UCC/EAN Composite / RSS"));
		return TRUE;
	}
	else if (chCodeID == 'w')
	{
		SetWindowText( hEditMessage2, TEXT("Data Matrix"));
		return TRUE;
	}
	else if (chCodeID == 'K')
	{
		SetWindowText( hEditMessage2, TEXT("Dutch Postal"));
		return TRUE;
	}
	else if (chCodeID == 'D')
	{
		SetWindowText( hEditMessage2, TEXT("EAN8"));
		return TRUE;
	}
	else if (chCodeID == 'f')
	{
		SetWindowText( hEditMessage2, TEXT("IATA 2of5"));
		return TRUE;
	}
	else if (chCodeID == 'e')
	{
		SetWindowText( hEditMessage2, TEXT("Interleaved 2of5"));
		return TRUE;
	}
	else if (chCodeID == 'J')
	{
		SetWindowText( hEditMessage2, TEXT("Japanese Postal"));
		return TRUE;
	}
	else if (chCodeID == 'x')
	{
		SetWindowText( hEditMessage2, TEXT("Maxicode"));
		return TRUE;
	}
	else if (chCodeID == 'R')
	{
		SetWindowText( hEditMessage2, TEXT("MicroPDF"));
		return TRUE;
	}
	else if (chCodeID == 'g')
	{
		SetWindowText( hEditMessage2, TEXT("MSI"));
		return TRUE;
	}
	else if (chCodeID == 'r')
	{
		SetWindowText( hEditMessage2, TEXT("PDF417"));
		return TRUE;
	}
	else if (chCodeID == 'L')
	{
		SetWindowText( hEditMessage2, TEXT("Planet Code"));
		return TRUE;
	}
	else if (chCodeID == 'P')
	{
		SetWindowText( hEditMessage2, TEXT("Postnet"));
		return TRUE;
	}
	else if (chCodeID == 'O')
	{
		SetWindowText( hEditMessage2, TEXT("OCR"));
		return TRUE;
	}
	else if (chCodeID == 's')
	{
		SetWindowText( hEditMessage2, TEXT("QR"));
		return TRUE;
	}
	else if (chCodeID == 'T')
	{
		SetWindowText( hEditMessage2, TEXT("TLC 39"));
		return TRUE;
	}
	else if (chCodeID == 'c')
	{
		SetWindowText( hEditMessage2, TEXT("UPC versions A"));
		return TRUE;
	}
	else if (chCodeID == 'E')
	{
		SetWindowText( hEditMessage2, TEXT("UPC versions E0,E1"));
		return TRUE;
	}
	else
	{
		SetWindowText( hEditMessage2, TEXT("Undefined Code"));
		return TRUE;
	}
}

