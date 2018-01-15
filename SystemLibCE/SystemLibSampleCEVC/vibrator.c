//=============================================================================
// CASIO Mobile Device SystemLib Sample                                       +
// Copyright (C) 2006 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
//=============================================================================
// vibrator.c : vibration sample                                              +
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

typedef struct tagVIBRATORSETTING{
	DWORD	dwMute;
	DWORD	dwNum;
	DWORD	dwOnTime;
	DWORD	dwOffTime;
} VIBRATORSETTING;


// global variable
extern HINSTANCE			hInst;			// current instance
static int					nSelType;		// current selected vibration type
static VIBRATORSETTING		OldSetting[6];	// setting before change
static VIBRATORSETTING		NewSetting[6];	// setting after change

const static TYPELIST TypeList[] = {
	{B_ALARM,		IDS_TYPE_ALARM},
	{B_WARNING,		IDS_TYPE_WARNING},
	{B_SCANEND,		IDS_TYPE_SCANEND},
	{B_WIREREAD,	IDS_TYPE_WIREREAD},
	{B_USERDEF,		IDS_TYPE_USERDEF},
	{B_ALL,			IDS_TYPE_ALLVIB},
};


// function proto-type definition
extern void				ShowErrorMessage	(HWND, LPCTSTR, DWORD);
static BOOL				GetVibratorSetting	(HWND);
static BOOL				SetVibratorSetting	(HWND, DWORD, VIBRATORSETTING*);
static BOOL				SetUserDefSetting	(HWND, int);
static BOOL				InitVibratorDlg		(HWND);
static BOOL				ChangeTypeSelect	(HWND, int);


// vibration dialog message routine
LRESULT CALLBACK VibratorDlgProc(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	int nIndex;
	LONG nChecked;
	DWORD dwRet;
	int i;

	switch (message)
	{
	case WM_INITDIALOG:
		// get setting and initialize dialog
		GetVibratorSetting(hDlg);
		InitVibratorDlg(hDlg);
		return TRUE;

	case WM_COMMAND:
		switch(LOWORD(wParam))
		{
		case IDC_TYPE:
			if(HIWORD(wParam) == CBN_SELENDOK){
				// vibration type change
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
			SetVibratorSetting(hDlg, TypeList[nSelType].dwType, &NewSetting[nSelType]);
			break;

		case IDC_NUM:
		case IDC_ONTIME:
		case IDC_OFFTIME:
			if(HIWORD(wParam) == EN_KILLFOCUS)
			{
				// count, vibration on/off time change
				SetUserDefSetting(hDlg, nSelType);
			}
			break;

		case IDC_PLAY:
			// play
			dwRet = SysPlayVibrator(
				TypeList[nSelType].dwType,
				NewSetting[nSelType].dwNum,
				NewSetting[nSelType].dwOnTime,
				NewSetting[nSelType].dwOffTime);
			ShowErrorMessage(hDlg, L"SysPlayVibrator", dwRet);
			break;

		case IDC_STOP:
			// stop
			SysStopVibrator();
			break;

		case IDCANCEL:
			// return before setting
			for(i = 0 ; i < dim(TypeList); i++)
			{
				SetVibratorSetting(hDlg, TypeList[i].dwType, &OldSetting[i]);
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


// get buzzer setting
static BOOL GetVibratorSetting(HWND hDlg)
{
	DWORD dwMute;
	int i;

	// variable initialize
	nSelType = 0;
	memset(OldSetting, 0x00, sizeof(OldSetting));
	memset(NewSetting, 0x00, sizeof(NewSetting));

	// get mute setting
	for(i = 0 ; i < dim(TypeList); i++)
	{
		dwMute = SysGetVibratorMute(TypeList[i].dwType);
		ShowErrorMessage(hDlg, L"SysGetVibratorMute", dwMute);
		OldSetting[i].dwMute = dwMute;
	}

	// copy setting
	memcpy(NewSetting, OldSetting, sizeof(OldSetting));
	return TRUE;
}


// change vibration setting
static BOOL SetVibratorSetting(HWND hDlg, DWORD dwType, VIBRATORSETTING *pSetting)
{
	DWORD dwRet;

	// change mute setting
	dwRet = SysSetVibratorMute(dwType, pSetting->dwMute);
	ShowErrorMessage(hDlg, L"SysSetVibratorMute", dwRet);

	return TRUE;
}

// change user setting by editbox input value
static BOOL SetUserDefSetting(HWND hDlg, int nIndex)
{
	TCHAR szTemp[256];

	// if current vibration type is not user setting, do not execute anything.
	if(TypeList[nIndex].dwType != B_USERDEF)
	{
		return TRUE;
	}

	// get count
	GetWindowText(GetDlgItem(hDlg, IDC_NUM), szTemp, 256);
	NewSetting[nIndex].dwNum = wcstoul(szTemp, NULL, 0);

	// get vibration on time
	GetWindowText(GetDlgItem(hDlg, IDC_ONTIME), szTemp, 256);
	NewSetting[nIndex].dwOnTime = wcstoul(szTemp, NULL, 0);

	// get vibration off time
	GetWindowText(GetDlgItem(hDlg, IDC_OFFTIME), szTemp, 256);
	NewSetting[nIndex].dwOffTime = wcstoul(szTemp, NULL, 0);

	return TRUE;
}

// vibration dialog initialize
static BOOL InitVibratorDlg(HWND hDlg)
{
	TCHAR szTemp[MAX_LOADSTRING];
	int i;

	// vibration type combobox initialize
	for(i = 0 ; i < dim(TypeList); i++)
	{
		LoadString(hInst, TypeList[i].wID, szTemp, MAX_LOADSTRING);
		ComboBox_AddString(GetDlgItem(hDlg, IDC_TYPE), szTemp);
	}

	// initialize select vibration type setting
	ComboBox_SetCurSel(GetDlgItem(hDlg, IDC_TYPE), nSelType);
	ChangeTypeSelect(hDlg, 0);
	return TRUE;
}

// vibration type change
static BOOL ChangeTypeSelect(HWND hDlg, int nNewSel)
{
	TCHAR szTemp[256];

	// judge vibration type before changed is user setting or not
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

	// judge selected vibration type is user setting or not
	if(TypeList[nNewSel].dwType == B_USERDEF)
	{
		// vibration count
		wsprintf(szTemp, L"%d", NewSetting[nNewSel].dwNum);
		SetWindowText(GetDlgItem(hDlg, IDC_NUM), szTemp);
		EnableWindow(GetDlgItem(hDlg, IDC_NUM), TRUE);

		// vibration on time
		wsprintf(szTemp, L"%d", NewSetting[nNewSel].dwOnTime);
		SetWindowText(GetDlgItem(hDlg, IDC_ONTIME), szTemp);
		EnableWindow(GetDlgItem(hDlg, IDC_ONTIME), TRUE);

		// vibration off time
		wsprintf(szTemp, L"%d", NewSetting[nNewSel].dwOffTime);
		SetWindowText(GetDlgItem(hDlg, IDC_OFFTIME), szTemp);
		EnableWindow(GetDlgItem(hDlg, IDC_OFFTIME), TRUE);
	}
	else
	{
		// disable vibration count
		SetWindowText(GetDlgItem(hDlg, IDC_NUM), L"");
		EnableWindow(GetDlgItem(hDlg, IDC_NUM), FALSE);
		
		// disable vibration on time
		SetWindowText(GetDlgItem(hDlg, IDC_ONTIME), L"");
		EnableWindow(GetDlgItem(hDlg, IDC_ONTIME), FALSE);

		// disable vibration off time
		SetWindowText(GetDlgItem(hDlg, IDC_OFFTIME), L"");
		EnableWindow(GetDlgItem(hDlg, IDC_OFFTIME), FALSE);
	}

	// judge selected vibration type is full vibration or not
	if(TypeList[nNewSel].dwType == B_ALL)
	{
		EnableWindow(GetDlgItem(hDlg, IDC_PLAY), FALSE);
	}
	else
	{
		EnableWindow(GetDlgItem(hDlg, IDC_PLAY), TRUE);
	}

	// select vibration type change
	nSelType = nNewSel;
	return TRUE;
}
