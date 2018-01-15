//=============================================================================
// CASIO Mobile Device SystemLib Sample                                       +
// Copyright (C) 2006 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
//=============================================================================
// led.c : LED sample                                                         +
//-----------------------------------------------------------------------------
#include <windows.h>
#include <commctrl.h>
#include <SystemLib.h>
#include "resource.h"

#define dim(x)				(sizeof(x) / sizeof(x[0]))

typedef struct tagLEDINFO{
	int		nID;
	DWORD	dwMode;
	LPCTSTR	pszMode;
} LEDINFO;

// LED dialog message routine
LRESULT CALLBACK LedDlgProc(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	DWORD dwLedMode;
	DWORD dwNum;
	DWORD dwOnTime;
	DWORD dwOffTime;
	DWORD dwRet;
	TCHAR szTemp[256];
	
	const LEDINFO LEDInfo[]={
		{IDC_GREEN,		LED_GREEN,		L"LED_GREEN"},
		{IDC_RED,		LED_RED,		L"LED_RED"},
		{IDC_ORANGE,	LED_ORANGE,		L"LED_ORANGE"},
		{IDC_BLUE,		LED_BLUE,		L"LED_BLUE"},
		{IDC_CYAN,		LED_CYAN,		L"LED_CYAN"},
		{IDC_MAGENTA,	LED_MAGENTA,	L"LED_MAGENTA"},
		{IDC_OFF,		LED_OFF,		L"LED_OFF"},
	};
	int i;

	switch (message)
	{
	case WM_INITDIALOG:
		SetWindowText(GetDlgItem(hDlg, IDC_NUM), L"1");
		SetWindowText(GetDlgItem(hDlg, IDC_ONTIME), L"16");
		SetWindowText(GetDlgItem(hDlg, IDC_OFFTIME), L"16");
		return TRUE;

	case WM_COMMAND:
		switch(LOWORD(wParam))
		{
		case IDC_GREEN:
		case IDC_RED:
		case IDC_ORANGE:
		case IDC_BLUE:
		case IDC_CYAN:
		case IDC_MAGENTA:
		case IDC_OFF:
			// LED ON count get
			GetWindowText(GetDlgItem(hDlg, IDC_NUM), szTemp, 255);
			dwNum = wcstoul(szTemp, NULL, 0);

			// LED ON time get
			GetWindowText(GetDlgItem(hDlg, IDC_ONTIME), szTemp, 255);
			dwOnTime = wcstoul(szTemp, NULL, 0);

			// LED OFF time get
			GetWindowText(GetDlgItem(hDlg, IDC_OFFTIME), szTemp, 255);
			dwOffTime = wcstoul(szTemp, NULL, 0);

			// LED ON/OFF
			for(i = 0 ; i < dim(LEDInfo) ; i++)
			{
				if(LEDInfo[i].nID == LOWORD(wParam))
				{
					dwLedMode = LEDInfo[i].dwMode;
					dwRet = SysSetLED(dwLedMode, dwNum, dwOnTime, dwOffTime);
					break;
				}
			}
			break;

		case IDC_GET:
			// get LED ON/OFF condition
			dwRet = SysGetLED();
			for(i = 0 ; i < dim(LEDInfo) ; i++)
			{
				if(LEDInfo[i].dwMode == dwRet)
				{
					MessageBox(hDlg, LEDInfo[i].pszMode, L"SysGetLED", MB_OK|MB_ICONINFORMATION);
					break;
				}
			}
			break;

		case IDOK:
		case IDCANCEL:
			// LED OFF then close dialog
			dwRet = SysSetLED(LED_OFF, 0, 0, 0);
			EndDialog(hDlg, LOWORD(wParam));
			return TRUE;
		}
		break;
	}
	return FALSE;
}
