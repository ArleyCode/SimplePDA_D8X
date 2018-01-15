//=============================================================================
// CASIO Mobile Device SystemLib Sample                                       +
// Copyright (C) 2006 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
//=============================================================================
// buzzer.c : buzzer sample                                                  +
//-----------------------------------------------------------------------------
#include <windows.h>
#include <windowsx.h>
#include <commctrl.h>
#include <SystemLib.h>
#include "resource.h"

#define MAX_LOADSTRING		100
#define dim(x)				(sizeof(x) / sizeof(x[0]))
#define BUZZER_KEY			L"Drivers\\BuiltIn\\Buzzer\\UserDef"

// structure definition
typedef struct tagTYPELIST{
	DWORD	dwType;
	UINT	wID;
} TYPELIST;

typedef struct tagBUZZERSETTING{
	DWORD	dwMute;
	DWORD	dwVol;
	DWORD	dwHelz;
	DWORD	dwTime;
} BUZZERSETTING;


// global variable
extern HINSTANCE			hInst;			// current instance
static int					nSelType;		// current selected buzzer type
static BUZZERSETTING		OldSetting[7];	// setting before change
static BUZZERSETTING		NewSetting[7];	// setting after change

const static TYPELIST TypeList[] = {
	{B_CLICK,		IDS_TYPE_CLICK},
	{B_ALARM,		IDS_TYPE_ALARM},
	{B_WARNING,		IDS_TYPE_WARNING},
	{B_SCANEND,		IDS_TYPE_SCANEND},
	{B_USERDEF,		IDS_TYPE_USERDEF},
	{B_TAP,			IDS_TYPE_TAP},
	{B_ALL,			IDS_TYPE_ALLBUZ},
};


// function proto-type definition
extern void				ShowErrorMessage	(HWND, LPCTSTR, DWORD);
static BOOL				GetBuzzerSetting	(HWND);
static BOOL				SetBuzzerSetting	(HWND, DWORD, BUZZERSETTING*);
static BOOL				SetUserDefSetting	(HWND, int);
static BOOL				InitBuzzerDlg		(HWND);
static BOOL				ChangeTypeSelect	(HWND, int);


// buzzer dialog message routine
LRESULT CALLBACK BuzzerDlgProc(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	int nIndex;
	LONG nChecked;
	DWORD dwRet;
	int i;

	switch (message)
	{
	case WM_INITDIALOG:
		// get setting and initialize dialog
		GetBuzzerSetting(hDlg);
		InitBuzzerDlg(hDlg);
		return TRUE;

	case WM_COMMAND:
		switch(LOWORD(wParam))
		{
		case IDC_TYPE:
			if(HIWORD(wParam) == CBN_SELENDOK){
				// change buzzer type
				nIndex = ComboBox_GetCurSel((HWND)lParam);
				ChangeTypeSelect(hDlg, nIndex);
			}
			break;

		case IDC_MUTE:
			// mute setting change
			nChecked = Button_GetCheck((HWND)lParam);
			if(nChecked == 0)
			{
				NewSetting[nSelType].dwMute = FALSE;
			}
			else
			{
				NewSetting[nSelType].dwMute = TRUE;
			}
			SetBuzzerSetting(hDlg, TypeList[nSelType].dwType, &NewSetting[nSelType]);
			break;

		case IDC_HELZ:
		case IDC_TIME:
			if(HIWORD(wParam) == EN_KILLFOCUS)
			{
				// frequency time change
				SetUserDefSetting(hDlg, nSelType);
			}
			break;

		case IDC_VOLUME:
			if(HIWORD(wParam) == CBN_SELENDOK){
				// volume change
				nIndex = ComboBox_GetCurSel((HWND)lParam);
				NewSetting[nSelType].dwVol = nIndex;
				SetBuzzerSetting(hDlg, TypeList[nSelType].dwType, &NewSetting[nSelType]);
			}
			break;

		case IDC_PLAY:
			//play
			dwRet = SysPlayBuzzer(
				TypeList[nSelType].dwType,
				NewSetting[nSelType].dwHelz,
				NewSetting[nSelType].dwTime);
			ShowErrorMessage(hDlg, L"SysPlayBuzzer", dwRet);
			break;

		case IDC_STOP:
			//stop
			dwRet = SysStopBuzzer();
			ShowErrorMessage(hDlg, L"SysStopBuzzer", dwRet);
			break;

		case IDCANCEL:
			// return to before setting
			for(i = 0 ; i < dim(TypeList); i++)
			{
				SetBuzzerSetting(hDlg, TypeList[i].dwType, &OldSetting[i]);
			}
		case IDOK:
			// close dialog
			EndDialog(hDlg, LOWORD(wParam));
			return TRUE;
		}
		break;
	}
	return FALSE;
}


// buzzer setting get
static BOOL GetBuzzerSetting(HWND hDlg)
{
	DWORD dwMute;
	DWORD dwVol;
	int i;

	// variable initialization
	nSelType = 0;
	memset(OldSetting, 0x00, sizeof(OldSetting));
	memset(NewSetting, 0x00, sizeof(NewSetting));

	// mute setting and volume get
	for(i = 0 ; i < dim(TypeList); i++)
	{
		dwMute = SysGetBuzzerMute(TypeList[i].dwType);
		ShowErrorMessage(hDlg, L"SysGetBuzzerMute", dwMute);
		OldSetting[i].dwMute = dwMute;

		dwVol = SysGetBuzzerVolume(TypeList[i].dwType);
		ShowErrorMessage(hDlg, L"SysGetBuzzerVolume", dwVol);

		if(dwVol == BUZZERVOLUME_MIN)		OldSetting[i].dwVol = 0;
		else if(dwVol == BUZZERVOLUME_MID)	OldSetting[i].dwVol = 1;
		else if(dwVol == BUZZERVOLUME_MAX)	OldSetting[i].dwVol = 2;
	}

	// user setting frequency and time get
	for(i = 0 ; i < dim(TypeList); i++)
	{
		if(TypeList[i].dwType == B_USERDEF)
		{
			HKEY	hKey;
			DWORD	dwDisposition, dwData, dwType;
			LONG	lResult;

			// registory open
			lResult = RegCreateKeyEx(HKEY_LOCAL_MACHINE, BUZZER_KEY, 0, L"",
				REG_OPTION_NON_VOLATILE, KEY_ALL_ACCESS, NULL, &hKey, &dwDisposition);
			if(lResult != ERROR_SUCCESS)
			{
				return FALSE;
			}

			// frequency get
			dwData	= sizeof(DWORD);
			dwType	= REG_DWORD;
			lResult = RegQueryValueEx(hKey, L"Freq", NULL, &dwType, (LPBYTE)&OldSetting[i].dwHelz, &dwData);
			if(lResult!=ERROR_SUCCESS){
				RegCloseKey(hKey);
				return FALSE;
			}

			// time get
			dwData	= sizeof(DWORD);
			dwType	= REG_DWORD;
			lResult = RegQueryValueEx(hKey, L"Time", NULL, &dwType, (LPBYTE)&OldSetting[i].dwTime, &dwData);
			if(lResult!=ERROR_SUCCESS){
				RegCloseKey(hKey);
				return FALSE;
			}
			RegCloseKey(hKey);
			break;
		}
	}

	// copy setting
	memcpy(NewSetting, OldSetting, sizeof(OldSetting));
	return TRUE;
}


// change buzzer setting
static BOOL SetBuzzerSetting(HWND hDlg, DWORD dwType, BUZZERSETTING *pSetting)
{
	DWORD dwVol;
	DWORD dwRet;

	// change mute setting
	dwRet = SysSetBuzzerMute(dwType, pSetting->dwMute);
	ShowErrorMessage(hDlg, L"SysSetBuzzerMute", dwRet);

	// change volume setting
	if(pSetting->dwVol == 0)		dwVol = BUZZERVOLUME_MIN;
	else if(pSetting->dwVol == 1)	dwVol = BUZZERVOLUME_MID;
	else if(pSetting->dwVol == 2)	dwVol = BUZZERVOLUME_MAX;
	dwRet = SysSetBuzzerVolume(dwType, dwVol);
	ShowErrorMessage(hDlg, L"SysSetBuzzerVolume", dwRet);

	// if user setting, change registry frequency and time
	if(dwType == B_USERDEF)
	{
		HKEY hKey;
		DWORD dwDisposition;
		LONG lResult;

		// registry open
		lResult = RegCreateKeyEx(HKEY_LOCAL_MACHINE, BUZZER_KEY, 0, L"",
			REG_OPTION_NON_VOLATILE, KEY_ALL_ACCESS, NULL, &hKey, &dwDisposition);
		if(lResult != ERROR_SUCCESS)
		{
			return FALSE;
		}

		// frequency
		lResult = RegSetValueEx(hKey, L"Freq", 0, REG_DWORD, (LPBYTE)&pSetting->dwHelz, sizeof(DWORD));
		if(lResult != ERROR_SUCCESS)
		{
			return FALSE;
		}

		// time
		lResult = RegSetValueEx(hKey, L"Time", 0, REG_DWORD, (LPBYTE)&pSetting->dwTime, sizeof(DWORD));
		if(lResult != ERROR_SUCCESS)
		{
			return FALSE;
		}
		RegCloseKey(hKey);
	}
	return TRUE;
}

// change user setting by editbox input value
static BOOL SetUserDefSetting(HWND hDlg, int nIndex)
{
	TCHAR szTemp[256];
	BOOL bRet;

	// if current buzzer type is not user setting, do not execute anything
	if(TypeList[nIndex].dwType != B_USERDEF)
	{
		return TRUE;
	}
	
	//get frequency
	GetWindowText(GetDlgItem(hDlg, IDC_HELZ), szTemp, 256);
	NewSetting[nIndex].dwHelz = wcstoul(szTemp, NULL, 0);

	//get time
	GetWindowText(GetDlgItem(hDlg, IDC_TIME), szTemp, 256);
	NewSetting[nIndex].dwTime = wcstoul(szTemp, NULL, 0);

	//change setting
	bRet = SetBuzzerSetting(hDlg, TypeList[nIndex].dwType, &NewSetting[nIndex]);
	return bRet;
}


// buzzer dialog initialize
static BOOL InitBuzzerDlg(HWND hDlg)
{
	TCHAR szTemp[MAX_LOADSTRING];
	UINT wVolumeID[] = {IDS_VOLUME_MIN, IDS_VOLUME_MID, IDS_VOLUME_MAX};
	int i;

	// buzzer type combo box initialize
	for(i = 0 ; i < dim(TypeList); i++)
	{
		LoadString(hInst, TypeList[i].wID, szTemp, MAX_LOADSTRING);
		ComboBox_AddString(GetDlgItem(hDlg, IDC_TYPE), szTemp);
	}

	// voluem combo box initialize
	for(i = 0 ; i < dim(wVolumeID); i++)
	{
		LoadString(hInst, wVolumeID[i], szTemp, MAX_LOADSTRING);
		ComboBox_AddString(GetDlgItem(hDlg, IDC_VOLUME), szTemp);
	}

	// initialize select buzzer type setting
	ComboBox_SetCurSel(GetDlgItem(hDlg, IDC_TYPE), nSelType);
	ChangeTypeSelect(hDlg, 0);
	return TRUE;
}


// buzzer type change
static BOOL ChangeTypeSelect(HWND hDlg, int nNewSel)
{
	TCHAR szTemp[256];

	// judge buzzer type before change is user setting or not
	if(TypeList[nSelType].dwType == B_USERDEF)
	{
		SetUserDefSetting(hDlg, nSelType);
	}

	// mute setting
	if(NewSetting[nNewSel].dwMute == TRUE)
	{
		Button_SetCheck(GetDlgItem(hDlg, IDC_MUTE), BST_CHECKED);
	}
	else
	{
		Button_SetCheck(GetDlgItem(hDlg, IDC_MUTE), BST_UNCHECKED);
	}

	// volume
	ComboBox_SetCurSel(GetDlgItem(hDlg, IDC_VOLUME), NewSetting[nNewSel].dwVol);

	// judge selected buzzer type is user setting or not
	if(TypeList[nNewSel].dwType == B_USERDEF)
	{
		// frequency
		wsprintf(szTemp, L"%d", NewSetting[nNewSel].dwHelz);
		SetWindowText(GetDlgItem(hDlg, IDC_HELZ), szTemp);
		EnableWindow(GetDlgItem(hDlg, IDC_HELZ), TRUE);

		// time
		wsprintf(szTemp, L"%d", NewSetting[nNewSel].dwTime);
		SetWindowText(GetDlgItem(hDlg, IDC_TIME), szTemp);
		EnableWindow(GetDlgItem(hDlg, IDC_TIME), TRUE);
	}
	else
	{
		// disable frequency
		SetWindowText(GetDlgItem(hDlg, IDC_HELZ), L"");
		EnableWindow(GetDlgItem(hDlg, IDC_HELZ), FALSE);

		// disable time
		SetWindowText(GetDlgItem(hDlg, IDC_TIME), L"");
		EnableWindow(GetDlgItem(hDlg, IDC_TIME), FALSE);
	}

	// judge selected buzzer type is max volume or not
	if(TypeList[nNewSel].dwType == B_ALL)
	{
		EnableWindow(GetDlgItem(hDlg, IDC_VOLUME), FALSE);
		EnableWindow(GetDlgItem(hDlg, IDC_PLAY), FALSE);
	}
	else
	{
		EnableWindow(GetDlgItem(hDlg, IDC_VOLUME), TRUE);
		EnableWindow(GetDlgItem(hDlg, IDC_PLAY), TRUE);
	}

	// change selected buzzer type
	nSelType = nNewSel;
	return TRUE;
}
