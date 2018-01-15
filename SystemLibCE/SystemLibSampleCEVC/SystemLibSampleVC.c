//=============================================================================
// CASIO Mobile Device SystemLib Sample                                       +
// Copyright (C) 2006 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
//=============================================================================
// main.c : main routine                                                        +
//-----------------------------------------------------------------------------
#include <windows.h>
#include <commctrl.h>
#include <SystemLib.h>
#include "resource.h"

#define MAX_LOADSTRING 100

// global variable
HINSTANCE				hInst;			// current instance
HWND					hwndCB;			// command bar handle

// function proto-type definition
BOOL					InitInstance		(HINSTANCE, int);
ATOM					MyRegisterClass		(HINSTANCE, LPTSTR);
LRESULT CALLBACK		WndProc				(HWND, UINT, WPARAM, LPARAM);
void					ShowErrorMessage	(HWND, LPCTSTR, DWORD);

extern LRESULT CALLBACK	LedDlgProc			(HWND, UINT, WPARAM, LPARAM);
extern LRESULT CALLBACK	BuzzerDlgProc		(HWND, UINT, WPARAM, LPARAM);
extern LRESULT CALLBACK	VibratorDlgProc		(HWND, UINT, WPARAM, LPARAM);


// application entry point
int WINAPI WinMain(	HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpCmdLine, int nCmdShow)
{
	MSG msg;

	// application initialize
	if (!InitInstance (hInstance, nCmdShow)) 
	{
		return FALSE;
	}

	// message loop
	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return msg.wParam;
}


// instance handle save and main window create
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	HWND	hWnd;
	TCHAR	szTitle[MAX_LOADSTRING];
	TCHAR	szClass[MAX_LOADSTRING];

	// save instance handle into global variable
	hInst = hInstance;

	// entry window class
	LoadString(hInstance, IDS_WND_CLASS, szClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance, szClass);

	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	hWnd = CreateWindow(szClass, szTitle, WS_VISIBLE,
		CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
		NULL, NULL, hInstance, NULL);

	if(!hWnd)
	{
		return FALSE;
	}

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);
	if(hwndCB)
	{
		CommandBar_Show(hwndCB, TRUE);
	}
	return TRUE;
}


// entry window class
ATOM MyRegisterClass(HINSTANCE hInstance, LPTSTR szClass)
{
	WNDCLASS	wc;

	wc.style			= CS_HREDRAW | CS_VREDRAW;
	wc.lpfnWndProc		= (WNDPROC) WndProc;
	wc.cbClsExtra		= 0;
	wc.cbWndExtra		= 0;
	wc.hInstance		= hInstance;
	wc.hIcon			= NULL;
	wc.hCursor			= 0;
	wc.hbrBackground	= (HBRUSH) GetStockObject(WHITE_BRUSH);
	wc.lpszMenuName		= 0;
	wc.lpszClassName	= szClass;

	return RegisterClass(&wc);
}


// main window message routine
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int wmId;
	int wmEvent;

	HDC hdc;
	RECT rt;
	PAINTSTRUCT ps;
	TCHAR szText[MAX_LOADSTRING];

	switch (message)
	{
	case WM_COMMAND:
		wmId    = LOWORD(wParam); 
		wmEvent = HIWORD(wParam); 
		switch (wmId)
		{
		case IDM_LED:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_LED), hWnd, (DLGPROC)LedDlgProc);
			break;

		case IDM_BUZZER:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_BUZZER), hWnd, (DLGPROC)BuzzerDlgProc);
			break;

		case IDM_VIBRATOR:
			DialogBox(hInst, MAKEINTRESOURCE(IDD_VIBRATOR), hWnd, (DLGPROC)VibratorDlgProc);
			break;

		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		break;

	case WM_CREATE:
		hwndCB = CommandBar_Create(hInst, hWnd, 1);
		CommandBar_InsertMenubar(hwndCB, hInst, IDM_MENU, 0);
		CommandBar_AddAdornments(hwndCB, 0, 0);
//		CommandBar_Show(hwndCB, TRUE);
		break;

	case WM_PAINT:
		hdc = BeginPaint(hWnd, &ps);
		GetClientRect(hWnd, &rt);
		LoadString(hInst, IDS_APP_TITLE, szText, MAX_LOADSTRING);
		DrawText(hdc, szText, _tcslen(szText), &rt, DT_SINGLELINE | DT_VCENTER | DT_CENTER);
		EndPaint(hWnd, &ps);
		break;

	case WM_DESTROY:
		CommandBar_Destroy(hwndCB);
		PostQuitMessage(0);
		break;

	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}

// error message display
void ShowErrorMessage(HWND hWnd, LPCTSTR pszCaption, DWORD dwStatus)
{
	TCHAR szTitle[MAX_LOADSTRING];
	TCHAR szMsg[MAX_LOADSTRING];
	UINT idError;

	// message box caption
	if(pszCaption && wcslen(pszCaption))
	{
		wcscpy(szTitle, pszCaption);
	}
	else
	{
		LoadString(hInst, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	}

	//error message
	if(dwStatus == SYS_PARAMERR)			idError = IDS_ERR_PARAMETER;
	else if(dwStatus == FUNCTION_UNSUPPORT)	idError = IDS_ERR_UNSUPPORT;
	else									return;

	// message box display
	LoadString(hInst, idError, szMsg, MAX_LOADSTRING);
	MessageBox(hWnd, szMsg, szTitle, MB_OK | MB_ICONSTOP);
}
