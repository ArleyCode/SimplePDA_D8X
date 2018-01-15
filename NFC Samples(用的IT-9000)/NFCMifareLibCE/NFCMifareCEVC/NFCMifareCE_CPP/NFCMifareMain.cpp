/***************************************************************************
 * <Description>
 * NFCMifareMain.cpp (Casio Computer Co., Ltd.)
 * -------------------------------------------------------------------------
 * <Function outline>
 * NFC Mifare IC Card reading Demo program
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
 * 1.0.0    2010.05.27      CASIO       000000      Original      
 * 1.0.1    2012.05.22      CASIO       000001      Modify        
 * 
 ***************************************************************************/
//	000000__	Original
//	__000000	Original

#include <windows.h>
#include "resource1.h"

//	000000__	Original
//  CASIO Libaray
#include "NFCMifareLib.h"
#include "NFCLib.h"
#include "SystemLib.h"
//	__000000	Original


BOOL DlgNFC(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam);
BOOL NFC_Scan(HWND hDlg);
BOOL DispErrorMessage(HWND hDlg, int iRet, TCHAR* szCommand);

int		iRet;

BYTE	pATQ[2];
BYTE	pSAK[1];
BYTE	pUid[7];
BYTE	pUidLen[1];
BYTE	pData[16];

// Block Number 1 for Authentication Key
TCHAR	szBlockNumber1[10];
int		iBlockNumberSize1;
int		iBlockNumber1;

// Block Number 2 for Data accessing
TCHAR	szBlockNumber2[10];
int		iBlockNumberSize2;
int		iBlockNumber2;

// Check for Mifare Standard or Ultralight (4 bytes length or not)
TCHAR	szUidLen[10];
int		iUidLenSize;
int		iUidLen;

// Authentication Key
TCHAR	szAKey[30];  
long	nAKeySize;

TCHAR	szWork1[10];
TCHAR	szWork2[10];
TCHAR	szWork3[10];
TCHAR	szWork4[10];
TCHAR	szWork5[10];
TCHAR	szWork6[10];

BYTE	bAKey[6];
long	lAKey[6];

// Authentication Key (Key A (= 0) or Key B (= 1))
int		iMode;

// For display status information

TCHAR	szMessage[500];
TCHAR	szMessage2[500];

// For display Mifare card data information 
TCHAR	szBuffer[261];


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
		SendMessage(GetDlgItem(hDlg,IDC_COMMAND), CB_ADDSTRING, 0, (LPARAM)_T("WRITE_Standard"));
		SendMessage(GetDlgItem(hDlg,IDC_COMMAND), CB_ADDSTRING, 0, (LPARAM)_T("WRITE_Ultralight"));
		SendMessage(GetDlgItem(hDlg,IDC_COMMAND), CB_SETCURSEL, 0, 0);

		SendMessage(GetDlgItem(hDlg,IDC_KEYAB), CB_ADDSTRING, 0, (LPARAM)_T("KEY_A"));
		SendMessage(GetDlgItem(hDlg,IDC_KEYAB), CB_ADDSTRING, 0, (LPARAM)_T("KEY_B"));
		SendMessage(GetDlgItem(hDlg,IDC_KEYAB), CB_SETCURSEL, 0, 0);

		SetDlgItemText(hDlg, IDC_BLOCKNUMBER1, _T("1"));
		SetDlgItemText(hDlg, IDC_BLOCKNUMBER2, _T("1"));

		SetDlgItemText(hDlg, IDC_A_KEY, _T("A0A1A2A3A4A5"));

		//	000001__	Modify
		// NFCMifare OPEN
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareOpen...start.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);

		iRet = NFCMifareOpen(NULL);

		if (iRet != NFC_OK)
		{
			DispErrorMessage(hDlg, iRet, _T("NFCMifareOpen"));
			return (INT_PTR)TRUE;
		}

		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareOpen...OK.\r\n"));
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
		// NFCMifare CLOSE
		NFCMifareClose();
		//	__000001	Modify
		break;
	}
	return FALSE;
}

BOOL NFC_Scan(HWND hDlg)
{
	EnableWindow(GetDlgItem(hDlg,IDC_NFC_SCAN),false);

	SetDlgItemText(hDlg, IDC_STATUS,  _T(""));
	SetDlgItemText(hDlg, IDC_ATQ,  _T(""));
	SetDlgItemText(hDlg, IDC_SAK,  _T(""));
	SetDlgItemText(hDlg, IDC_UID,  _T(""));
	SetDlgItemText(hDlg, IDC_UIDLEN,  _T(""));
	SetDlgItemText(hDlg, IDC_DATA, _T(""));

	StringCchCopy( szMessage, sizeof(szMessage), _T(""));
	StringCchCopy( szMessage2, sizeof(szMessage2), _T(""));
			
	UpdateWindow(hDlg);


	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifarePolling...start.\r\n"));
	StringCchCat( szMessage, sizeof(szMessage), szMessage2);
	SetDlgItemText(hDlg, IDC_STATUS, szMessage);

	// NFCMifare POLLING
	iRet = NFCMifarePolling(10000 , NULL, 0);

	if (iRet != NFC_OK)
	{
		DispErrorMessage(hDlg, iRet, _T("NFCMifarePolling"));
		return FALSE;
	}
	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifarePolling...OK.\r\n"));
	StringCchCat( szMessage, sizeof(szMessage), szMessage2);
	SetDlgItemText(hDlg, IDC_STATUS, szMessage);


	//	000001__	Modify
	ZeroMemory(pATQ, sizeof(pATQ));
	ZeroMemory(pSAK, sizeof(pSAK));
	ZeroMemory(pUid, sizeof(pUid));
	ZeroMemory(pUidLen, sizeof(pUidLen));
	//	__000001	Modify

	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareGetCardResponse...start.\r\n"));
	StringCchCat( szMessage, sizeof(szMessage), szMessage2);
	SetDlgItemText(hDlg, IDC_STATUS, szMessage);


	// NFCMifare GET CARD RESPONSE
	iRet = NFCMifareGetCardResponse(pATQ, pSAK, pUid, pUidLen, 0);

	if (iRet != NFC_OK)
	{
		DispErrorMessage(hDlg, iRet, _T("NFCMifareGetCardResponse"));
		return FALSE;
    }

	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareGetCardResponse...OK.\r\n"));
	StringCchCat( szMessage, sizeof(szMessage), szMessage2);
	SetDlgItemText(hDlg, IDC_STATUS, szMessage);


	//	000001__	Modify
	ZeroMemory(szBuffer, sizeof(szBuffer));
	//	__000001	Modify

	for (int i=0 ; i<2 ; i++)
	{
		wsprintf(&szBuffer[i*3], _T("%02X "), pATQ[i]);
	}
	SetDlgItemText(hDlg, IDC_ATQ, szBuffer );

	//	000001__	Modify
	ZeroMemory(szBuffer, sizeof(szBuffer));
	//	__000001	Modify

	for (int i=0 ; i<1 ; i++)
	{
		wsprintf(&szBuffer[i*3], _T("%02X "), pSAK[i]);
	}
	SetDlgItemText(hDlg, IDC_SAK, szBuffer );


	//	000001__	Modify
	ZeroMemory(szBuffer, sizeof(szBuffer));
	//	__000001	Modify
	for (int i=0 ; i<1 ; i++)
	{
		wsprintf(&szBuffer[i*3], _T("%02X "), pUidLen[i]);
	}
	SetDlgItemText(hDlg, IDC_UIDLEN, szBuffer );


	//	000001__	Modify
	ZeroMemory(szBuffer, sizeof(szBuffer));
	for (int i=0 ; i<(int)pUidLen[0] ; i++)
	//	__000001	Modify
	{
		wsprintf(&szBuffer[i*3], _T("%02X "), pUid[i]);
	}
	SetDlgItemText(hDlg, IDC_UID, szBuffer );

	ZeroMemory( szUidLen, sizeof( szUidLen ) );
	iUidLenSize = sizeof( szUidLen );

	GetDlgItemText( hDlg, IDC_UIDLEN, szUidLen, iUidLenSize );

	iUidLen = ::_ttoi(szUidLen);

		//	000001__	Modify
		ZeroMemory(szWork1, sizeof(szWork1));
		ZeroMemory(szWork2, sizeof(szWork2));
		ZeroMemory(szWork3, sizeof(szWork3));
		ZeroMemory(szWork4, sizeof(szWork4));
		ZeroMemory(szWork5, sizeof(szWork5));
		ZeroMemory(szWork6, sizeof(szWork6));
		//	__000001	Modify

		ZeroMemory( szBlockNumber1, sizeof(szBlockNumber1));
		iBlockNumberSize1 = sizeof( szBlockNumber1 );

	if (iUidLen == 4)
	{
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareAuthentication...start.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);


		if (SendMessage(GetDlgItem(hDlg,IDC_KEYAB), CB_GETCURSEL,0 ,0 ) == 0 )
		{
			iMode = NFC_MIFARE_KEYA;
		}
		else
		{
			iMode = NFC_MIFARE_KEYB;
		}

		ZeroMemory( szAKey, sizeof(szAKey));
		nAKeySize = sizeof( szAKey );

		GetDlgItemText(hDlg, IDC_A_KEY, szAKey, nAKeySize);

		for (int i=0; i<2 ; i++)
		{
			szWork1[i]=szAKey[i];
			szWork2[i]=szAKey[i+2];
			szWork3[i]=szAKey[i+4];
			szWork4[i]=szAKey[i+6];
			szWork5[i]=szAKey[i+8];
			szWork6[i]=szAKey[i+10];
		}
		szWork1[2] = NULL;
		szWork2[2] = NULL;
		szWork3[2] = NULL;
		szWork4[2] = NULL;
		szWork5[2] = NULL;
		szWork6[2] = NULL;


		lAKey[0] = ::_tcstol( szWork1, NULL, 16 );
		lAKey[1] = ::_tcstol( szWork2, NULL, 16 );
		lAKey[2] = ::_tcstol( szWork3, NULL, 16 );
		lAKey[3] = ::_tcstol( szWork4, NULL, 16 );
		lAKey[4] = ::_tcstol( szWork5, NULL, 16 );
		lAKey[5] = ::_tcstol( szWork6, NULL, 16 );

		for (int i=0; i<6; i++)
		{
			//	000001__	Modify
			bAKey[i] = (BYTE)lAKey[i];
			//	__000001	Modify
		}

		GetDlgItemText( hDlg, IDC_BLOCKNUMBER1, szBlockNumber1, iBlockNumberSize1 );

		iBlockNumber1 = ::_ttoi(szBlockNumber1);

		iRet = NFCMifareAuthentication( iMode, bAKey, iBlockNumber1, 0);

		if (iRet != NFC_OK)
		{
			DispErrorMessage(hDlg, iRet, _T("NFCMifareAuthentication"));
			return FALSE;
		}

		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareAuthentication...OK.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);

	}


	// NFCMifareRead

	if (SendMessage(GetDlgItem(hDlg, IDC_COMMAND), CB_GETCURSEL,0 ,0 ) == 0)
	{
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareRead...start.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);


		ZeroMemory( szBlockNumber2, sizeof( szBlockNumber2 ) );
		iBlockNumberSize2 = sizeof( szBlockNumber2 );

		GetDlgItemText( hDlg, IDC_BLOCKNUMBER2, szBlockNumber2, iBlockNumberSize2 );

		iBlockNumber2 = ::_ttoi(szBlockNumber2);


		iRet = NFCMifareRead(iBlockNumber2, pData, 0);

		if (iRet != NFC_OK)
		{
			DispErrorMessage(hDlg, iRet, _T("NFCMifareRead"));
			return FALSE;
		}

		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareRead...OK.\r\n"));
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
	// NFCMifareWrite
	else if (SendMessage(GetDlgItem(hDlg,IDC_COMMAND), CB_GETCURSEL,0 ,0 ) == 1)  // Case of WRITE
	{
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareWrite...start.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);


		// Append your NFCMifareWrite code.


		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareWrite...OK.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
	}
	// NFCMifareWrite4 (Mifare Ultralight)
	else if (SendMessage(GetDlgItem(hDlg,IDC_COMMAND), CB_GETCURSEL,0 ,0 ) == 2)  // Case of WRITE
	{
		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareWrite4...start.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);


		// Append your NFCMifareWrite4 code.


		StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
		StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareWrite4...OK.\r\n"));
		StringCchCat( szMessage, sizeof(szMessage), szMessage2);
		SetDlgItemText(hDlg, IDC_STATUS, szMessage);
	}


	// NFCMifare RADIO OFF
	iRet = NFCMifareRadioOff();

	if (iRet != NFC_OK)
	{
		DispErrorMessage(hDlg, iRet, _T("NFCMifareRadioOff"));
		return FALSE;
	}

	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifareRadioOff...OK.\r\n"));
	StringCchCat( szMessage, sizeof(szMessage), szMessage2);
	SetDlgItemText(hDlg, IDC_STATUS, szMessage);


	SysSetBuzzerVolume(B_SCANEND, BUZZERVOLUME_MAX);
	SysPlayBuzzer(B_SCANEND, B_USERDEF, B_USERDEF);

	StringCchCopy( szMessage2, sizeof(szMessage2), szMessage);
	StringCchCopy( szMessage, sizeof(szMessage), _T("NFCMifare scanning...OK.\r\n"));
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