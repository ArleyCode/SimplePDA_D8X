/***************************************************************************
 * <Description>
 * NFCFeliCaMain.cpp (Casio Computer Co., Ltd.)
 * -------------------------------------------------------------------------
 * <Function outline>
 * NFC FeliCa IC Card reading Demo program
 * -------------------------------------------------------------------------
 * <Language>
 * Visual C++
 * -------------------------------------------------------------------------
 * <Develop Environment>
 * VS 2005
 * -------------------------------------------------------------------------
 * <Target>
 * DT-5300 CE / IT-9000 CE
 * -------------------------------------------------------------------------
 * Copyright(C)2009 CASIO COMPUTER CO.,LTD. All rights reserved.
 * -------------------------------------------------------------------------
 * <History>
 * Version  Date            Company     Keyword     Comment
 * 1.0.0    2010.05.25      CASIO       000000      Original      
 * 1.0.1    2012.05.22      CASIO       000001      Modify        
 * 
 ***************************************************************************/
//	000000__	Original
//	__000000	Original

#include <windows.h>
#include "resource.h"
//#include "stdlib.h"

//	000000__	Original
//  CASIO Libaray
#include "NFCFelicaLib.h"
#include "NFCLib.h"
#include "SystemLib.h"
//	__000000	Original


BOOL DlgNFC(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL NFC_Scan(HWND hDlg);
BOOL DispErrorMessage(HWND hDlg, int iRet, TCHAR* szCommand);

int iRet;

// parameter for NFCFelicaGetCardResponse function
BYTE pIDm[8];
BYTE pPMm[8];
DWORD pSystemCode;

// parameter for NFCFelicaRead function
TCHAR  szServiceCode[10];  
TCHAR* pszServiceCodeEnd;
int    nServiceCodeSize;
long	nServiceCode;

TCHAR  szBlockNumber[10];
int    nBlockNumberSize;
int		iBlockNumber;

BYTE pData[16];

// For display status information
TCHAR szMessage[500];
TCHAR szMessage2[500];

// For display Mifare card data information 
TCHAR szBuffer[261];


int WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
			LPWSTR lpCmdLine, int nShowCmd)
{
	DialogBoxW(hInstance, MAKEINTRESOURCE( IDD_NFC ), NULL, DlgNFC);
	return 0;
}

BOOL DlgNFC(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg)
	{
	case WM_INITDIALOG:
		// Task bar hide
		ShowWindow(FindWindow(_T("HHTaskBar"), _T("")), SW_HIDE);

		SendMessage(GetDlgItem(hDlg,IDC_COMMAND), CB_ADDSTRING, 0, (LPARAM)_T("READ"));
		SendMessage(GetDlgItem(hDlg,IDC_COMMAND), CB_ADDSTRING, 0, (LPARAM)_T("WRITE"));
		SendMessage(GetDlgItem(hDlg,IDC_COMMAND), CB_SETCURSEL, 0, 0);
		SetDlgItemText(hDlg, IDC_SERVICECODE, _T("1009"));
		SetDlgItemText(hDlg, IDC_BLOCKNUMBER, _T("1"));
		
		//	000001__	Modify
		// NFCFeliCa OPEN
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFelicaOpen...start.\r\n"));
		StringCchCopy( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);

		iRet = NFCFelicaOpen(NULL);

		if (iRet != NFC_OK)
		{
			DispErrorMessage(hDlg, iRet, _T("NFCFelicaOpen"));
			return (INT_PTR)TRUE;
		}

		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFelicaOpen...OK.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
		//	__000001	Modify

        return (INT_PTR)TRUE;

	case WM_COMMAND:
		switch (LOWORD(wParam))
		{
		case IDC_EXIT:
			// Task bar show
			ShowWindow(FindWindow(_T("HHTaskBar"), _T("")), SW_SHOW);

			EndDialog(hDlg, LOWORD(wParam));
			break;

		case IDC_NFC_SCAN:
			NFC_Scan(hDlg);
			break;
		}
		break;
	case WM_CLOSE:
		//	000001__	Modify
		ShowWindow(FindWindow(_T("HHTaskBar"), _T("")), SW_SHOW);

		EndDialog(hDlg, LOWORD(wParam));
		//	__000001	Modify
		break;
	case WM_DESTROY:
		//	000001__	Modify
		// NFCFelica CLOSE
		NFCFelicaClose();
		//	__000001	Modify
		break;
	}
	return FALSE;
}


BOOL NFC_Scan(HWND hDlg)
{
	EnableWindow(GetDlgItem(hDlg,IDC_NFC_SCAN),false);

	SetDlgItemText(hDlg, IDC_STATUS,  _T(""));
	SetDlgItemText(hDlg, IDC_SYSTEM_CODE,  _T(""));
	SetDlgItemText(hDlg, IDC_IDM,  _T(""));
	SetDlgItemText(hDlg, IDC_PMM,  _T(""));
	SetDlgItemText(hDlg, IDC_DATA, _T(""));

	StringCchCopy( szMessage, sizeof(szMessage), _T(""));
	StringCchCopy( szMessage2, sizeof(szMessage2), _T(""));
			
	UpdateWindow(hDlg);

	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFelicaPolling...start.\r\n"));
	StringCchCat( szMessage, sizeof(szMessage), szMessage2);
	SetDlgItemText(hDlg, IDC_STATUS, szMessage);

	// NFCFeliCa POLLING
	iRet = NFCFelicaPolling(10000 , NULL , 0xffff,0);

	if (iRet != NFC_OK)
	{
		DispErrorMessage(hDlg, iRet, _T("NFCFelicaPolling"));
		return FALSE;
	}

	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFelicaPolling...OK.\r\n"));
	StringCchCat( szMessage, sizeof(szMessage), szMessage2);
	SetDlgItemText(hDlg, IDC_STATUS, szMessage);

	//	000001__	Modify
	ZeroMemory(pIDm, sizeof(pIDm));
	ZeroMemory(pPMm, sizeof(pPMm));
	//	__000001	Modify

	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFelicaGetCardResponse...start.\r\n"));
	StringCchCat( szMessage, sizeof(szMessage), szMessage2);
	SetDlgItemText(hDlg, IDC_STATUS, szMessage);


	// NFCFelica GET CARD RESPONSE
	iRet = NFCFelicaGetCardResponse(pIDm,pPMm,&pSystemCode, 0);

	if (iRet != NFC_OK)
	{
		DispErrorMessage(hDlg, iRet, _T("NFCFelicaGetCardResponse"));
		return FALSE;
    }

	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFelicaGetCardResponse...OK.\r\n"));
	StringCchCat( szMessage, sizeof(szMessage), szMessage2);
	SetDlgItemText(hDlg, IDC_STATUS, szMessage);

	//	000001__	Modify
	ZeroMemory(szBuffer, sizeof(szBuffer));
	//	__000001	Modify

	wsprintf(szBuffer, _T("%2d"), pSystemCode);
	SetDlgItemText(hDlg, IDC_SYSTEM_CODE, szBuffer );

	//	000001__	Modify
	ZeroMemory(szBuffer, sizeof(szBuffer));
	//	__000001	Modify

	for (int i=0 ; i<8 ; i++)
	{
		wsprintf(&szBuffer[i*3], _T("%02X "), pIDm[i]);
	}
	SetDlgItemText(hDlg, IDC_IDM, szBuffer );

	//	000001__	Modify
	ZeroMemory(szBuffer, sizeof(szBuffer));
	//	__000001	Modify

	for (int i=0 ; i<8 ; i++)
	{
		wsprintf(&szBuffer[i*3], _T("%02X "), pPMm[i]);
	}
	SetDlgItemText(hDlg, IDC_PMM, szBuffer );


	// NFCFelicaRead
	if (SendMessage(GetDlgItem(hDlg,IDC_COMMAND), CB_GETCURSEL,0 ,0 ) == 0)  // Case of READ
	{
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFelicaRead...start.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);

		ZeroMemory( szServiceCode, sizeof( szServiceCode ) );
		nServiceCodeSize = sizeof( szServiceCode );

		ZeroMemory( szBlockNumber, sizeof( szBlockNumber ) );
		nBlockNumberSize = sizeof( szBlockNumber );

		GetDlgItemText( hDlg, IDC_SERVICECODE, szServiceCode, nServiceCodeSize );
		GetDlgItemText( hDlg, IDC_BLOCKNUMBER, szBlockNumber, nBlockNumberSize );

		nServiceCode = ::_tcstol(szServiceCode,&pszServiceCodeEnd,16);
		iBlockNumber = ::_ttoi(szBlockNumber);


		iRet = NFCFelicaRead(nServiceCode, iBlockNumber, pData, 0);

		if (iRet != NFC_OK)
		{
			DispErrorMessage(hDlg, iRet, _T("NFCFelicaRead"));
			return FALSE;
		}

		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFelicaRead...OK.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);


		//	000001__	Modify
		ZeroMemory(szBuffer, sizeof(szBuffer));
		//	__000001	Modify

		for (int i=0 ; i<16 ; i++)
		{
			wsprintf(&szBuffer[i*2], _T("%02X"), pData[i]);
		}
		SetDlgItemText(hDlg, IDC_DATA, szBuffer );
	}
	// NFCFelicaWrite
	else if (SendMessage(GetDlgItem(hDlg,IDC_COMMAND), CB_GETCURSEL,0 ,0 ) == 1)  // Case of WRITE
	{
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFelicaWrite...start.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);


		// Append your NFCFelicaWrite code.


		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFelicaWrite...OK.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
	}

	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFelicaRadioOff...start.\r\n"));
	StringCchCat( szMessage, sizeof(szMessage), szMessage2);
	SetDlgItemText(hDlg, IDC_STATUS, szMessage);


	// NFCFelica RADIO OFF
	iRet = NFCFelicaRadioOff();

	if (iRet != NFC_OK)
	{
		DispErrorMessage(hDlg, iRet, _T("NFCFelicaRadioOff"));
		return FALSE;
	}

	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFelicaRadioOff...OK.\r\n"));
	StringCchCat( szMessage, sizeof(szMessage), szMessage2);
	SetDlgItemText(hDlg, IDC_STATUS, szMessage);


	SysSetBuzzerVolume(B_SCANEND, BUZZERVOLUME_MAX);
	SysPlayBuzzer(B_SCANEND, B_USERDEF, B_USERDEF);

	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCFeliCa scanning...OK.\r\n"));
	StringCchCat( szMessage, sizeof(szMessage), szMessage2);
	SetDlgItemText(hDlg, IDC_STATUS, szMessage);

	EnableWindow(GetDlgItem(hDlg,IDC_NFC_SCAN),true);

	return TRUE;
}

BOOL DispErrorMessage(HWND hDlg, int iRet, TCHAR* szCommand)
{
	switch (iRet)
	{
	case NFC_PON:
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), szCommand);
		StringCchCat( szMessage, sizeof(szMessage), _T(" : NFC already opened.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
		break;
	case NFC_NOT_DEVICE:
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), szCommand);
		StringCchCat( szMessage, sizeof(szMessage), _T(" : NFC driver error.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
		break;
	case NFC_ERROR_INVALID_ACCESS:
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), szCommand);
		StringCchCat( szMessage, sizeof(szMessage), _T(" : Device exclusive error.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
		break;
	case NFC_ERROR_MODULE:
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), szCommand);
		StringCchCat( szMessage, sizeof(szMessage), _T(" : NFC module not responce error.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
		break;
	case NFC_POF:
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), szCommand);
		StringCchCat( szMessage, sizeof(szMessage), _T(" : NFC not open error.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
		break;
	case NFC_PRM:
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), szCommand);
		StringCchCat( szMessage, sizeof(szMessage), _T(" : NFC parameter error.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
		break;
	case NFC_ERROR_TIMEOUT:
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), szCommand);
		StringCchCat( szMessage, sizeof(szMessage), _T(" : Timeout error.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
		break;
	case NFC_ERROR_CALLBACK:
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), szCommand);
		StringCchCat( szMessage, sizeof(szMessage), _T(" : Call back function error.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
		break;
	case NFC_ERROR_STOP:
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), szCommand);
		StringCchCat( szMessage, sizeof(szMessage), _T(" : Stop error by stop function.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
		break;
	case NFC_NOT_ACTIVATION:
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), szCommand);
		StringCchCat( szMessage, sizeof(szMessage), _T(" : Card don't start error.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
	    break;
	default:
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), szCommand);
		StringCchCat( szMessage, sizeof(szMessage), _T(" : Other error.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
		break;

	}
	SysSetBuzzerVolume(B_WARNING, BUZZERVOLUME_MAX);
	SysPlayBuzzer(B_WARNING, B_USERDEF, B_USERDEF);

	EnableWindow(GetDlgItem(hDlg,IDC_NFC_SCAN),true);
	return TRUE;
}