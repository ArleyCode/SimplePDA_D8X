// JpegSample.cpp : Defines the entry point for the application.
//

#include "stdafx.h"
#include "JpegSampleVC.h"
#include <commctrl.h>
#include <Commdlg.h>

#include "JpegCe.h"

#define MAX_LOADSTRING 100

#define IDS_STATIC		-1
#define IDC_ENCODEBTN1	1001
#define IDC_ENCODEBTN2	1002
#define IDC_DECODEBTN1	1003
#define IDC_DECODEBTN2	1004


// Global Variables:
HINSTANCE			hInst;			// The current instance

HANDLE				hEncodeBtn1,hEncodeBtn2,hDecodeBtn1,hDecodeBtn2;

// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass	(HINSTANCE, LPTSTR);
BOOL				InitInstance	(HINSTANCE, int);
LRESULT CALLBACK	WndProc			(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK	DlgProc			(HWND, UINT, WPARAM, LPARAM);

int OpenDialog(HWND hWnd, TCHAR *tFileName,int FILE_TYPE);
int SaveDialog(HWND hWnd, TCHAR *tFileName,int FILE_TYPE);

BITMAP g_Bitmap;
BOOL DecodeJpegFile( HWND hWnd);
BOOL DecodeJpegMemory( HWND hWnd);
BOOL EncodeBmpFile( HWND hWnd);
BOOL EncodeBmpMemory( HWND hWnd);
int ShowBitmap(HWND hWnd, HDC hDC, BITMAP *bm);
int OpenBitmapFile(TCHAR *tFileName, BITMAPINFOHEADER *InfoHeader, BYTE **bRGB, DWORD *dwBuffSize);

int WINAPI WinMain(	HINSTANCE hInstance,
					HINSTANCE hPrevInstance,
					LPTSTR    lpCmdLine,
					int       nCmdShow)
{
	MSG msg;

	// Perform application initialization:
	if (!InitInstance (hInstance, nCmdShow)) 
	{
		return FALSE;
	}


	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0)) 
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return msg.wParam;
}

//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
//  COMMENTS:
//
//    It is important to call this function so that the application 
//    will get 'well formed' small icons associated with it.
//
ATOM MyRegisterClass(HINSTANCE hInstance, LPTSTR szWindowClass)
{
	WNDCLASS	wc;

    wc.style			= CS_HREDRAW | CS_VREDRAW;
    wc.lpfnWndProc		= (WNDPROC) WndProc;
    wc.cbClsExtra		= 0;
    wc.cbWndExtra		= 0;
    wc.hInstance		= hInstance;
    wc.hIcon			= LoadIcon(hInstance, MAKEINTRESOURCE(IDI_JPEGSAMPLE));
    wc.hCursor			= 0;
    wc.hbrBackground	= (HBRUSH)GetStockObject(LTGRAY_BRUSH);
    wc.lpszMenuName		= 0;
    wc.lpszClassName	= szWindowClass;

	return RegisterClass(&wc);
}

//
//  FUNCTION: InitInstance(HANDLE, int)
//
//  PURPOSE: Saves instance handle and creates main window
//
//  COMMENTS:
//
//    In this function, we save the instance handle in a global variable and
//    create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	HWND	hWnd;
	TCHAR	szTitle[MAX_LOADSTRING];			// The title bar text
	TCHAR	szWindowClass[MAX_LOADSTRING];		// The window class name

	hInst = hInstance;		// Store instance handle in our global variable
	// Initialize global strings
	LoadString(hInstance, IDC_JPEGSAMPLE, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance, szWindowClass);

	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	hWnd = CreateWindow(szWindowClass, szTitle, WS_CAPTION | WS_VISIBLE | WS_SYSMENU,
						0, 0, 240, 320, NULL, NULL, hInstance, NULL);

	if (!hWnd)
	{	
		return FALSE;
	}

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	return TRUE;
}

//
//  FUNCTION: WndProc(HWND, unsigned, WORD, LONG)
//
//  PURPOSE:  Processes messages for the main window.
//
//  WM_COMMAND	- process the application menu
//  WM_PAINT	- Paint the main window
//  WM_DESTROY	- post a quit message and return
//
//
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	HDC hdc;
	int wmId, wmEvent;
	PAINTSTRUCT ps;

	switch (message) 
	{
		case WM_COMMAND:
			wmId    = LOWORD(wParam); 
			wmEvent = HIWORD(wParam); 
			switch (wmId)
			{

				case IDC_ENCODEBTN1:
					if(EncodeBmpFile(hWnd) != TRUE){
						MessageBox(hWnd,_T("ENCODE1 ERROR"),_T("ERROR"),MB_OK);
					}
					break;

				case IDC_ENCODEBTN2:
					if(EncodeBmpMemory(hWnd) != TRUE){
						MessageBox(hWnd,_T("ENCODE2 ERROR"),_T("ERROR"),MB_OK);
					}
					break;

				case IDC_DECODEBTN1:
					if(DecodeJpegFile(hWnd) != TRUE){
						MessageBox(hWnd,_T("DECODE1 ERROR"),_T("ERROR"),MB_OK);
					}
					break;

				case IDC_DECODEBTN2:
					if(DecodeJpegMemory(hWnd) != TRUE){
						MessageBox(hWnd,_T("DECODE2 ERROR"),_T("ERROR"),MB_OK);
					}
					break;

				default:
				   return DefWindowProc(hWnd, message, wParam, lParam);
			}
			break;
		case WM_CREATE:

			CreateWindow(TEXT("STATIC") , TEXT("JpegCe Sample Program") ,
						WS_CHILD | WS_VISIBLE | SS_CENTER ,	20 , 20 , 200 , 20 ,
						hWnd , (HMENU)-1 , hInst , NULL);


			hEncodeBtn1 = CreateWindow( TEXT("button"), TEXT("Convert BMP to JPEG [1]"),
				WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
				20, 50, 200, 30, hWnd, ( HMENU)IDC_ENCODEBTN1, hInst, NULL);

			hEncodeBtn2 = CreateWindow( TEXT("button"), TEXT("Convert BMP to JPEG [2]"),
				WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
				20, 90, 200, 30, hWnd, ( HMENU)IDC_ENCODEBTN2, hInst, NULL);

			hDecodeBtn1 = CreateWindow( TEXT("button"), TEXT("Display JPEG Image [1]"),
				WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
				20, 130, 200, 30, hWnd, ( HMENU)IDC_DECODEBTN1, hInst, NULL);

			hDecodeBtn2 = CreateWindow( TEXT("button"), TEXT("Display JPEG Image [2]"),
				WS_CHILD | WS_VISIBLE | BS_PUSHBUTTON,
				20, 170, 200, 30, hWnd, ( HMENU)IDC_DECODEBTN2, hInst, NULL);

			break;
		case WM_PAINT:

			hdc = BeginPaint(hWnd, &ps);

			EndPaint(hWnd, &ps); 

			break;
		case WM_DESTROY:

			PostQuitMessage(0);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
   }
   return 0;
}


LRESULT CALLBACK DlgProc(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	RECT rt, rt1;

	switch (message)
	{
		case WM_INITDIALOG:

			//Dialog size change
			GetClientRect(GetDesktopWindow(),&rt);
			SetWindowPos(hDlg, HWND_TOPMOST, rt.left , rt.top ,
					rt.right , rt.bottom , SWP_NOZORDER);

			//Close button position change
			GetClientRect(hDlg,&rt1);
			MoveWindow(GetDlgItem(hDlg,IDC_BCLOSE),
						((rt.right-80)/2),rt.bottom-40,
						80,20,TRUE);

			return TRUE;

		case WM_PAINT:
			PAINTSTRUCT	ps;
			HDC hdc;

			hdc = BeginPaint(hDlg,&ps);

			ShowBitmap(hDlg,hdc,&g_Bitmap);

			EndPaint(hDlg,&ps);

			break;

		case WM_COMMAND:
			switch(LOWORD(wParam)){
			case IDC_BCLOSE:
				EndDialog(hDlg,LOWORD(wParam));
				return TRUE;

			default:
				break;
			}

		default:
			break;

	}
    return FALSE;
}


//Create FileOpenDialog
int OpenDialog(HWND hWnd, TCHAR *tFileName,int FILE_TYPE)
{
	OPENFILENAME	OFN;

	ZeroMemory(&OFN, sizeof(OPENFILENAME));
	OFN.lStructSize = sizeof(OPENFILENAME); 
	OFN.hwndOwner   = hWnd;
	OFN.lpstrFilter = TEXT("ALL Files(*.*)\0*.*\0\0");
	if(FILE_TYPE == FILE_JPEG){
		OFN.lpstrFilter = TEXT("JPEG Files(*.jpg)\0*.jpg\0\0");
	}
	if(FILE_TYPE == FILE_BMP){
		OFN.lpstrFilter = TEXT("BMP Files(*.bmp)\0*.bmp\0\0");
	}
	OFN.lpstrFile   = tFileName;  
	OFN.nMaxFile    = MAX_PATH;
	OFN.Flags       = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST | OFN_HIDEREADONLY;    
	OFN.lpstrTitle  = TEXT("Open File");
	if(FILE_TYPE == FILE_JPEG){
		OFN.lpstrTitle  = TEXT("Open JPEG File");
	}
	if(FILE_TYPE == FILE_BMP){
		OFN.lpstrTitle  = TEXT("Open BMP File");
	}

	return ( GetOpenFileName(&OFN));

}

//Create FileSaveDialog
int SaveDialog(HWND hWnd, TCHAR *tFileName,int FILE_TYPE)
{
	int lRet;
	OPENFILENAME	OFN;

	tFileName[0] = '\0';
	ZeroMemory(&OFN, sizeof(OPENFILENAME));
	OFN.lStructSize = sizeof(OPENFILENAME); 
	OFN.hwndOwner   = hWnd;
	OFN.lpstrFilter = TEXT("ALL Files(*.*)\0*.*\0\0");
	if(FILE_TYPE == FILE_JPEG){
		OFN.lpstrFilter = TEXT("JPEG Files(*.jpg)\0*.jpg\0\0");
	}
	if(FILE_TYPE == FILE_BMP){
		OFN.lpstrFilter = TEXT("BMP Files(*.bmp)\0*.bmp\0\0");
	}
	OFN.lpstrFile   = tFileName;  
	OFN.nMaxFile    = MAX_PATH;
	OFN.Flags       = OFN_OVERWRITEPROMPT | OFN_HIDEREADONLY;    
	OFN.lpstrTitle  = TEXT("Save File");
	if(FILE_TYPE == FILE_JPEG){
		OFN.lpstrTitle  = TEXT("Save JPEG File");
	}
	if(FILE_TYPE == FILE_BMP){
		OFN.lpstrTitle  = TEXT("Save BMP File");
	}
	if(FILE_TYPE == FILE_JPEG){
		OFN.lpstrDefExt = TEXT("jpg");
	}
	if(FILE_TYPE == FILE_BMP){
		OFN.lpstrDefExt = TEXT("bmp");
	}

	lRet = GetSaveFileName(&OFN);
	return (lRet);
}


//A Jpeg file is screen-displayed.
BOOL DecodeJpegFile( HWND hWnd)
{
	HANDLE hFile, hImage;
	TCHAR tFileName[ MAX_PATH + 1];
	
	ZeroMemory(tFileName,MAX_PATH+1);
	
	if ( !OpenDialog( hWnd, tFileName,FILE_JPEG))
	{
		return TRUE;
	}

	SetCursor( LoadCursor( NULL, IDC_WAIT));

	//JPEG file opening
	hFile = CreateFile( tFileName, GENERIC_READ, 0,
		NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hFile == INVALID_HANDLE_VALUE){
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}

	//JPEG File Decode Function
	hImage = JPGDecodeFromFile( hFile, &g_Bitmap);
	if ((hImage == (void *)JPG_DECODE_ERROR) || (hImage == (void *)JPG_INVALID_PARAM)){
		CloseHandle(hFile);
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}

	//File Close
	CloseHandle( hFile);

	SetCursor( LoadCursor( NULL, IDC_ARROW));
	
	DialogBox(hInst, (LPCTSTR)IDD_BITMAPDLG, hWnd, (DLGPROC)DlgProc);

	//Decode Memory opening
	JPGRelease( hImage);

	return TRUE;
}


//A Jpeg Data is screen-displayed.
BOOL DecodeJpegMemory( HWND hWnd)
{
	int iRet;

	HANDLE hFile, hImage;
	TCHAR tFileName[ MAX_PATH + 1];

	BYTE *buff;
	DWORD dwBuffSize;
	DWORD dwReadSize;
	
	ZeroMemory(tFileName,MAX_PATH+1);
	
	if ( !OpenDialog( hWnd, tFileName,FILE_JPEG))
	{
		return TRUE;
	}

	SetCursor( LoadCursor( NULL, IDC_WAIT));


	//JPEG file opening
	hFile = CreateFile( tFileName, GENERIC_READ, 0,
		NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hFile == INVALID_HANDLE_VALUE){
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}

	

	//Get Buffer
	dwBuffSize = GetFileSize(hFile ,NULL);
	if(dwBuffSize == INVALID_FILE_SIZE){
		CloseHandle(hFile);
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}
	dwBuffSize = dwBuffSize + 100;
	buff = (BYTE *)VirtualAlloc(NULL,dwBuffSize,MEM_COMMIT,PAGE_READWRITE);
	if(buff == NULL){
		CloseHandle(hFile);
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}

	//Read JPEG File 
	iRet = ReadFile(hFile,buff,dwBuffSize,&dwReadSize,NULL);
	if(iRet == 0){
		VirtualFree(buff,dwBuffSize,MEM_DECOMMIT);
		CloseHandle(hFile);
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}	
	
	//File Close
	CloseHandle( hFile);

	//JPEG Data Decode Function
	hImage = JPGDecode(buff, dwBuffSize, &g_Bitmap);
	if ((hImage == (void *)JPG_DECODE_ERROR) || (hImage == (void *)JPG_INVALID_PARAM)){
		VirtualFree(buff,dwBuffSize,MEM_DECOMMIT);
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}

	//Memory opening
	VirtualFree(buff,dwBuffSize,MEM_DECOMMIT);

	SetCursor( LoadCursor( NULL, IDC_ARROW));
	
	DialogBox(hInst, (LPCTSTR)IDD_BITMAPDLG, hWnd, (DLGPROC)DlgProc);

	//Decode Memory opening
	JPGRelease( hImage);

	return TRUE;
}


//A Bitmap file is changed into a Jpeg file.
BOOL EncodeBmpFile( HWND hWnd)
{
	HANDLE hFile;
	BITMAPINFOHEADER InfoHeader;
	TCHAR tFileName[ MAX_PATH + 1];
	JPEGINFO JpegInfo;

	BYTE *buff;
	DWORD dwBuffSize;
	
	ZeroMemory(tFileName,MAX_PATH+1);
	
	if ( !OpenDialog( hWnd, tFileName,FILE_BMP))
	{
		return TRUE;
	}

	SetCursor( LoadCursor( NULL, IDC_WAIT));

	//BITMAP file opening
	if(OpenBitmapFile(tFileName,&InfoHeader,&buff,&dwBuffSize) != TRUE){
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}

	SetCursor( LoadCursor( NULL, IDC_ARROW));

	if ( !SaveDialog(hWnd,tFileName,FILE_JPEG))
	{
		VirtualFree(buff,dwBuffSize,MEM_DECOMMIT);
		return TRUE;
	}

	SetCursor( LoadCursor( NULL, IDC_WAIT));

	//Preservation file creation
	hFile = CreateFile(tFileName , GENERIC_READ|GENERIC_WRITE , 0 , NULL ,
						CREATE_ALWAYS , FILE_ATTRIBUTE_NORMAL , NULL);
	if (hFile == INVALID_HANDLE_VALUE){
		VirtualFree(buff,dwBuffSize,MEM_DECOMMIT);
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}

	//JPEGINFO structure object setup
	JpegInfo.dwWidth = InfoHeader.biWidth;
	JpegInfo.dwHeight = InfoHeader.biHeight; 
	JpegInfo.dwValidWidth = InfoHeader.biWidth;
	JpegInfo.dwValidHeight = InfoHeader.biHeight;
	JpegInfo.dwComponents = 3;
	JpegInfo.dwColorSpace = JPG_RGB;
	JpegInfo.dwQuality = 100;
	JpegInfo.dwBaseLine = JPG_BASELINE;
	//Create Jpeg File
	if(JPGEncodeToFile(hFile,buff,&JpegInfo) != JPG_SUCCESS){
		VirtualFree(buff,dwBuffSize,MEM_DECOMMIT);
		CloseHandle(hFile);
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}

	CloseHandle(hFile);

	VirtualFree(buff,dwBuffSize,MEM_DECOMMIT);

	SetCursor( LoadCursor( NULL, IDC_ARROW));

	return TRUE;
}


//A Bitmap Memory is changed into a Jpeg file.
BOOL EncodeBmpMemory( HWND hWnd)
{
	int iRet;

	HANDLE hFile,hImage;
	BITMAPINFOHEADER InfoHeader;
	TCHAR tFileName[ MAX_PATH + 1];
	JPEGINFO JpegInfo;

	BYTE *buff;
	BYTE *EncData;
	DWORD dwBuffSize;
	DWORD dwJpegSize;
	DWORD dwWriteSize;
	
	ZeroMemory(tFileName,MAX_PATH+1);
	
	if ( !OpenDialog( hWnd, tFileName,FILE_BMP))
	{
		return TRUE;
	}

	SetCursor( LoadCursor( NULL, IDC_WAIT));

	//BITMAP file opening
	if(OpenBitmapFile(tFileName,&InfoHeader,&buff,&dwBuffSize) != TRUE){
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}

	//JPEGINFO structure object setup
	JpegInfo.dwWidth = InfoHeader.biWidth;
	JpegInfo.dwHeight = InfoHeader.biHeight; 
	JpegInfo.dwValidWidth = InfoHeader.biWidth;
	JpegInfo.dwValidHeight = InfoHeader.biHeight;
	JpegInfo.dwComponents = 3;
	JpegInfo.dwColorSpace = JPG_RGB;
	JpegInfo.dwQuality = 100;
	JpegInfo.dwBaseLine = JPG_BASELINE;
	//Create Jpeg File
	hImage = JPGEncode(&EncData, buff, &dwJpegSize,&JpegInfo);
	if ((hImage == (void *)JPG_DECODE_ERROR) || (hImage == (void *)JPG_INVALID_PARAM)){
		VirtualFree(buff,dwBuffSize,MEM_DECOMMIT);
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}

	SetCursor( LoadCursor( NULL, IDC_ARROW));

	if ( !SaveDialog(hWnd,tFileName,FILE_JPEG))
	{
		VirtualFree(buff,dwBuffSize,MEM_DECOMMIT);
		JPGRelease(hImage);
		return TRUE;
	}
	
	SetCursor( LoadCursor( NULL, IDC_WAIT));
	
	//Preservation file creation
	hFile = CreateFile(tFileName , GENERIC_READ|GENERIC_WRITE , 0 , NULL ,
						CREATE_ALWAYS , FILE_ATTRIBUTE_NORMAL , NULL);
	if (hFile == INVALID_HANDLE_VALUE){
		VirtualFree(buff,dwBuffSize,MEM_DECOMMIT);
		JPGRelease(hImage);
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}

	iRet = WriteFile(hFile,EncData,dwJpegSize,&dwWriteSize,NULL);
	if(iRet == 0){
		VirtualFree(buff,dwBuffSize,MEM_DECOMMIT);
		CloseHandle(hFile);
		JPGRelease(hImage);
		SetCursor( LoadCursor( NULL, IDC_ARROW));
		return FALSE;
	}


	CloseHandle(hFile);

	VirtualFree(buff,dwBuffSize,MEM_DECOMMIT);

	//Encode Memory opening
	JPGRelease(hImage);

	SetCursor( LoadCursor( NULL, IDC_ARROW));

	return TRUE;
}


//Bitmap File display Function
//HWND hWnd : 
//HDC hDC : Display place device context
//BITMAP *bm : The address to a BITMAP structure object
int ShowBitmap(HWND hWnd, HDC hDC, BITMAP *bm)
{
	BITMAPINFOHEADER InfoHeader;
	RECT rec;
	double width_tmp;
	double height_tmp;
	double BitmapWidth;
	double BitmapHeight;
	int ShowLine;

	// Makes the header of image information
	InfoHeader.biSize = ( DWORD)sizeof( BITMAPINFOHEADER);
	InfoHeader.biWidth = bm->bmWidth;
	InfoHeader.biHeight = bm->bmHeight;
	InfoHeader.biPlanes = bm->bmPlanes;
	InfoHeader.biBitCount = bm->bmBitsPixel;
	InfoHeader.biCompression = 0;
	InfoHeader.biSizeImage = 0;
	InfoHeader.biXPelsPerMeter = 0;
	InfoHeader.biYPelsPerMeter = 0;
	InfoHeader.biClrUsed = 0;
	InfoHeader.biClrImportant = 0;

	//Display position calculation
	GetClientRect(hWnd,&rec);

	BitmapWidth = bm->bmWidth;
	BitmapHeight = bm->bmHeight;
	width_tmp = rec.right - 20;
	height_tmp = width_tmp / (BitmapWidth / BitmapHeight);

	// Show the Image
	ShowLine = StretchDIBits(hDC,
							10, 
							30,
							(int)width_tmp,
							(int)height_tmp,
							0,
							0,
							bm->bmWidth,
							bm->bmHeight,
							bm->bmBits,
							(LPBITMAPINFO)&InfoHeader,
							DIB_RGB_COLORS,
							SRCCOPY);

	return ShowLine;
}


//Bitmap File Open Function
//TCHAR *tFileName : Open File Name
//BITMAPINFOHEADER *InfoHeader : The address to a BITMAPINFOHEADER structure object
//BYTE **bRGB : The address to a RGB data buffer acquisition place
//DWORD *dwBuffSize : The address to a buffer size storing place
int OpenBitmapFile(TCHAR *tFileName, BITMAPINFOHEADER *InfoHeader, BYTE **bRGB, DWORD *dwBuffSize)
{
	int iRet;
	HANDLE hFile;

	BITMAPFILEHEADER FileHeader;

	DWORD dwReadSize;
	DWORD LineSize;
	DWORD WidthByteSize;
	DWORD dwCnt;
	DWORD BuffOffset;
	
	//Bitmap File Open
	hFile = CreateFile(tFileName , GENERIC_READ , 0 , NULL ,
						OPEN_EXISTING , FILE_ATTRIBUTE_NORMAL , NULL);
	if (hFile == INVALID_HANDLE_VALUE){
		return FALSE;
	}

	//Read BitmapFileHeader
	iRet = ReadFile(hFile , &FileHeader , sizeof (BITMAPFILEHEADER) , &dwReadSize , NULL);  
	if(iRet == 0){
		CloseHandle(hFile);
		return FALSE;
	}
	//File format check
	if (FileHeader.bfType != 0x4D42) {
		CloseHandle(hFile);
		return FALSE;
	}

	//Read BitmapInfoHeader 
	iRet = ReadFile(hFile , InfoHeader , sizeof (BITMAPINFOHEADER) , &dwReadSize , NULL);
	if(iRet == 0){
		CloseHandle(hFile);
		return FALSE;
	}


	//RGB data reading
	*dwBuffSize = InfoHeader->biWidth * InfoHeader->biHeight * 3 + 100;
	LineSize = InfoHeader->biWidth *  3;
	WidthByteSize = (InfoHeader->biWidth * 3 + 3) & 0xfffffffcL;
	*bRGB = (BYTE *)VirtualAlloc(NULL,*dwBuffSize,MEM_COMMIT,PAGE_READWRITE);
	if(*bRGB == NULL){
		CloseHandle(hFile);
		return FALSE;
	}

	for(dwCnt = 0;dwCnt < (DWORD)InfoHeader->biHeight;dwCnt++){
		BuffOffset = dwCnt * LineSize;
		//One-line data reading
		iRet = ReadFile(hFile,&(*bRGB)[BuffOffset],WidthByteSize,&dwReadSize,NULL);
		if(iRet == 0){
			VirtualFree(*bRGB,*dwBuffSize,MEM_DECOMMIT);
			CloseHandle(hFile);
			return FALSE;
		}
	}

	CloseHandle(hFile);
	return TRUE;
}