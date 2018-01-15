/***************************************************************************
'* <Description>
'* OBReadLibSampleVC.cpp (Casio Computer Co., Ltd.)
'* -------------------------------------------------------------------------
'* <Function outline>
'* OBR Library Sample program
'* -------------------------------------------------------------------------
'* <Language>
'* C++
'* -------------------------------------------------------------------------
'* <Develop Environment>
'* VS 2005
'* -------------------------------------------------------------------------
'* <Target>
'* JPN : DT-5300CE / DT-X7 / DT-5200 / DT-X8
'* ENG : DT-X30CE / DT-X7 / DT-X11 / IT-600 / DT-X8
'* -------------------------------------------------------------------------
'* Copyright(C)2010 CASIO COMPUTER CO.,LTD. All rights reserved.
'* -------------------------------------------------------------------------
'* <History>
'* Version  Date            Company     Keyword     Comment
'* 1.0.0    2010.02.17      CASIO       000000      Original      
'* 
'***************************************************************************/

#include <afxwin.h>
#include <commctrl.h>
#include "OBReadLib.h"
#include "min_max.h"
#include "resource.h"

#define	WINCE_CLASSTAG		TEXT( "WCE_" )

#define MAX_LOADSTRING		100
#define	COMMANDBAR_HEIGHT	24
#define TASKBAR_HEIGHT		26

//=========== Case of Windows CE ================
#define CX_EDIT1			20
#define CY_EDIT1			20
#define CW_EDIT1			200
#define CH_EDIT1			70

#define CX_EDIT2			120
#define CY_EDIT2			100
#define CW_EDIT2			100
#define CH_EDIT2			20

#define CX_EDIT3			CX_EDIT2
#define CY_EDIT3			130
#define CW_EDIT3			CW_EDIT2
#define CH_EDIT3			CH_EDIT2

#define CX_EDIT4			CX_EDIT2
#define CY_EDIT4			160
#define CW_EDIT4			CW_EDIT2
#define CH_EDIT4			CH_EDIT2

#define CX_STATIC1			20
#define CY_STATIC1			CY_EDIT2
#define CW_STATIC1			95
#define CH_STATIC1			20
//===============================================

////=========== Case of Windows Mobile ============
//#define CX_EDIT1			40
//#define CY_EDIT1			40
//#define CW_EDIT1			400
//#define CH_EDIT1			140
//
//#define CX_EDIT2			240
//#define CY_EDIT2			200
//#define CW_EDIT2			200
//#define CH_EDIT2			40
//
//#define CX_EDIT3			CX_EDIT2
//#define CY_EDIT3			260
//#define CW_EDIT3			CW_EDIT2
//#define CH_EDIT3			CH_EDIT2
//
//#define CX_EDIT4			CX_EDIT2
//#define CY_EDIT4			320
//#define CW_EDIT4			CW_EDIT2
//#define CH_EDIT4			CH_EDIT2
//
//#define CX_STATIC1			40
//#define CY_STATIC1			CY_EDIT2
//#define CW_STATIC1			190
//#define CH_STATIC1			40
////=============================================

#define CX_STATIC2			CX_STATIC1
#define CY_STATIC2			CY_EDIT3
#define CW_STATIC2			CW_STATIC1
#define CH_STATIC2			CH_STATIC1

#define CX_STATIC3			CX_STATIC1
#define CY_STATIC3			CY_EDIT4
#define CW_STATIC3			CW_STATIC1
#define CH_STATIC3			CH_STATIC1


HINSTANCE	g_hInst;		// The current instance
HANDLE	hObrStatus;
HANDLE	hKillDispatchThread;
HANDLE	ghWatchThread;	

DWORD	KillRxThread1;
DWORD	KillRxThread2;

DWORD	gwRcodeKindTbl[] = {
	OBR_NONDT,
	OBR_CD39,
	OBR_NW_7,
	OBR_WPCA,
	OBR_WPC,
	OBR_UPEA,
	OBR_UPE,
	OBR_IDF,
	OBR_ITF,
	OBR_CD93,
	OBR_CD128,
	OBR_MSI,
	OBR_IATA,
	OBR_RSS14,
	OBR_RSSLTD,
	OBR_RSSEXP
} ;

LPCTSTR	tRcodeKind_Msg[] = {
	{L"            "},
	{L"OBR_CD39    "},
	{L"OBR_NW_7    "},
	{L"OBR_WPCA    "},
	{L"OBR_WPC     "},
	{L"OBR_UPEA    "},
	{L"OBR_UPE     "},
	{L"OBR_IDF     "},
	{L"OBR_ITF     "},
	{L"OBR_CD93    "},
	{L"OBR_CD128   "},
	{L"OBR_MSI     "},
	{L"OBR_IATA    "},
	{L"OBR_RSS14   "},
	{L"OBR_RSSLTD  "},
	{L"OBR_RSSEXP  "}
};


class CMainWnd : public CFrameWnd
{
public:
	CMainWnd();
	virtual ~CMainWnd();

private:
	afx_msg int		OnCreate( LPCREATESTRUCT lpCreateStruct );
	afx_msg void	OnClose( void );
	afx_msg void	OnDestroy( void );
	DECLARE_MESSAGE_MAP()

	CEdit		*m_pBaseEdit1;
	CEdit		*m_pBaseEdit2;
	CEdit		*m_pBaseEdit3;
	CEdit		*m_pBaseEdit4;
	CStatic		*m_pBaseStatic1;
	CStatic		*m_pBaseStatic2;
	CStatic		*m_pBaseStatic3;
};

BEGIN_MESSAGE_MAP( CMainWnd, CFrameWnd )
	ON_WM_CREATE()
	ON_WM_CLOSE()
	ON_WM_DESTROY()
END_MESSAGE_MAP()

class CMyApp : public CWinApp
{
public:
	virtual BOOL InitInstance();
};

// error message display
void ShowErrorMessage(int nMsg, int nTitle)
{
	CString szWindowClass;
	CString szWindowTitle;
	CString szMsg;
	CString szTitle;
	HWND hWnd;

	szWindowClass.LoadString(IDS_WND_CLASS);
	szWindowTitle.LoadString(IDS_APP_TITLE);
	hWnd = FindWindow(L"WCE_" + szWindowClass, szWindowTitle);

	szMsg.LoadString(nMsg);
	szTitle.LoadString(nTitle);

	MessageBox(hWnd, szMsg, szTitle, MB_OK|MB_ICONWARNING|MB_TOPMOST);
}

// read barcode kind display
VOID ReadBarKindDsp(HWND hWnd, DWORD wRcvCode) 
{
	int num;
	int i;

	SetWindowText(GetDlgItem(hWnd, IDC_EDIT_RCODE), tRcodeKind_Msg[0]);

	num = sizeof(gwRcodeKindTbl)/sizeof(gwRcodeKindTbl[0]);

	for (i = 0 ; i < num ; i++){
		if(wRcvCode == gwRcodeKindTbl[i]){
			SetWindowText(GetDlgItem(hWnd, IDC_EDIT_RCODE), tRcodeKind_Msg[i]);
			break;
		}
	}
	return;
}

// read execution thread routine 
DWORD LoopExproc(DWORD lpParam)
{
	HWND hWnd = (HWND)lpParam;
	BYTE byDecodeBuf[1024];
	TCHAR tDecodeData[1024];
	TCHAR szEndsts[200];
	DWORD dwRcvCode;
	BYTE byLeng;
	LPTSTR szRet ;
	int iRet;

	SetEvent(hKillDispatchThread) ;
	SetWindowText(GetDlgItem(hWnd, IDC_EDIT_ENDINF), L"0");

	while(1){
		WaitForSingleObject(hObrStatus, 50);
		if(KillRxThread1 == 1){
			break ;
		}

		memset(byDecodeBuf, 0x00, sizeof(byDecodeBuf));
		memset(tDecodeData, 0x00, sizeof(tDecodeData));
		dwRcvCode = OBR_NONDT;
		byLeng = 0;
		iRet = OBRGets(byDecodeBuf, &dwRcvCode, &byLeng);

		if(iRet != OBR_OK){
			// change to HEX code
			szRet = _itow(iRet, szEndsts, 16);
			SetWindowText(GetDlgItem(hWnd, IDC_EDIT_ENDINF), szEndsts);
		}
		if(dwRcvCode == OBR_NONDT){
			continue;
		}

		// display clear
		SetWindowText(GetDlgItem(hWnd,IDC_EDIT_SCANDATA), L"");
		SetWindowText(GetDlgItem(hWnd,IDC_EDIT_RCODE), L"");
		SetWindowText(GetDlgItem(hWnd,IDC_EDIT_RLNG), L"");

		// barcode data display
		MultiByteToWideChar(CP_ACP, MB_PRECOMPOSED, (LPCSTR)byDecodeBuf, 1024, tDecodeData, 1024);
		SetWindowText(GetDlgItem(hWnd, IDC_EDIT_SCANDATA), tDecodeData);

		// scan barcode type display
		ReadBarKindDsp(hWnd, dwRcvCode);

		// digit number display
		szRet = _itow((int)byLeng, szEndsts, 10) ;
		SetWindowText(GetDlgItem(hWnd, IDC_EDIT_RLNG), szEndsts);

		// end information display
		szRet = _itow(iRet, szEndsts, 16);
		SetWindowText(GetDlgItem(hWnd, IDC_EDIT_ENDINF), szEndsts);
	}
	SetEvent(hKillDispatchThread);
	WaitForSingleObject(hObrStatus, 100);
	KillRxThread2 = 1;
	return TRUE;
}

// scan start
int StartScanChk(HWND hDlg)
{
	TCHAR szEndsts[200];
	DWORD dwThreadID;
	LPTSTR szRet;
	int iRet = 0;

	hObrStatus			= CreateEvent(0,FALSE,FALSE,NULL);
	hKillDispatchThread	= CreateEvent(0, FALSE, FALSE, NULL);
	ghWatchThread		= CreateThread(0, 0, (LPTHREAD_START_ROUTINE)LoopExproc, hDlg, 0, &dwThreadID);
	if(!ghWatchThread){
		iRet = 9999;

		// change to HEX code
		szRet = _itow(iRet, szEndsts, 16);
		SetWindowText(GetDlgItem(hDlg, IDC_EDIT_ENDINF), szEndsts);
	}
	WaitForSingleObject(hKillDispatchThread, 3000);
	return iRet;
}

//initialize member variables
CMainWnd::CMainWnd()
{
	m_pBaseEdit1 = NULL;
	m_pBaseEdit2 = NULL;
	m_pBaseEdit3 = NULL;
	m_pBaseEdit4 = NULL;
	m_pBaseStatic1 = NULL;
	m_pBaseStatic2 = NULL;
	m_pBaseStatic3 = NULL;
}

//open member variables
CMainWnd::~CMainWnd()
{
	if(m_pBaseEdit1)	delete m_pBaseEdit1;
	if(m_pBaseEdit2)	delete m_pBaseEdit2;
	if(m_pBaseEdit3)	delete m_pBaseEdit3;
	if(m_pBaseEdit4)	delete m_pBaseEdit4;
	if(m_pBaseStatic1)	delete m_pBaseStatic1;
	if(m_pBaseStatic2)	delete m_pBaseStatic2;
	if(m_pBaseStatic3)	delete m_pBaseStatic3;
}


int CMainWnd::OnCreate(LPCREATESTRUCT lpcs)
{
	BOOL bRet[7];
	TCHAR szText1[MAX_LOADSTRING];
	TCHAR szText2[MAX_LOADSTRING];
	TCHAR szText3[MAX_LOADSTRING];
	LOGFONT	lf;
	DWORD dwStyle;
	DWORD dwExStyle;
	int i;
	int iRet;

	RECT rcCtrl[7] = {
		{CX_EDIT1,		CY_EDIT1,		CX_EDIT1 + CW_EDIT1,		CY_EDIT1 + CH_EDIT1},
		{CX_EDIT2,		CY_EDIT2,		CX_EDIT2 + CW_EDIT2,		CY_EDIT2 + CH_EDIT2},
		{CX_EDIT3,		CY_EDIT3,		CX_EDIT3 + CW_EDIT3,		CY_EDIT3 + CH_EDIT3},
		{CX_EDIT4,		CY_EDIT4,		CX_EDIT4 + CW_EDIT4,		CY_EDIT4 + CH_EDIT4},
		{CX_STATIC1,	CY_STATIC1,		CX_STATIC1 + CW_STATIC1,	CY_STATIC1 + CH_STATIC1},
		{CX_STATIC2,	CY_STATIC2,		CX_STATIC2 + CW_STATIC2,	CY_STATIC2 + CH_STATIC2},
		{CX_STATIC3,	CY_STATIC3,		CX_STATIC3 + CW_STATIC3,	CY_STATIC3 + CH_STATIC3},
	};

	GetObject( GetStockObject( SYSTEM_FONT ), sizeof( lf ), &lf );
	lf.lfHeight = -12;

	LoadString(g_hInst,	IDS_RCODE,	szText1,	MAX_LOADSTRING);
	LoadString(g_hInst,	IDS_RLNG,	szText2,	MAX_LOADSTRING);
	LoadString(g_hInst,	IDS_ENDINF,	szText3,	MAX_LOADSTRING);

	m_pBaseEdit1 = new CEdit;
	m_pBaseEdit2 = new CEdit;
	m_pBaseEdit3 = new CEdit;
	m_pBaseEdit4 = new CEdit;
	m_pBaseStatic1 = new CStatic;
	m_pBaseStatic2 = new CStatic;
	m_pBaseStatic3 = new CStatic;

	dwExStyle = WS_EX_CLIENTEDGE;
	dwStyle = WS_CHILD | WS_VISIBLE | ES_MULTILINE | ES_WANTRETURN | WS_VSCROLL;
	bRet[0] = m_pBaseEdit1->CreateEx(dwExStyle, L"EDIT", L"", dwStyle, rcCtrl[0], this, IDC_EDIT_SCANDATA);

	dwExStyle = WS_EX_CLIENTEDGE;
	dwStyle = WS_CHILD | WS_VISIBLE;
	bRet[1] = m_pBaseEdit2->CreateEx(dwExStyle, L"EDIT", L"", dwStyle, rcCtrl[1], this, IDC_EDIT_RCODE);
	bRet[2] = m_pBaseEdit3->CreateEx(dwExStyle, L"EDIT", L"", dwStyle, rcCtrl[2], this, IDC_EDIT_RLNG);
	bRet[3] = m_pBaseEdit4->CreateEx(dwExStyle, L"EDIT", L"", dwStyle, rcCtrl[3], this, IDC_EDIT_ENDINF);

	dwExStyle = 0;
	dwStyle = WS_CHILD | WS_VISIBLE;
	bRet[4] = m_pBaseStatic1->CreateEx(dwExStyle, L"STATIC", szText1, dwStyle, rcCtrl[4], this, IDC_STATIC);
	bRet[5] = m_pBaseStatic2->CreateEx(dwExStyle, L"STATIC", szText2, dwStyle, rcCtrl[5], this, IDC_STATIC);
	bRet[6] = m_pBaseStatic3->CreateEx(dwExStyle, L"STATIC", szText3, dwStyle, rcCtrl[6], this, IDC_STATIC);

	for(i = 0 ; i < 7 ; i++){
		if(!bRet[i]){
			return -1;
		}
	}

	iRet = OBRLoadConfigFile();			// ini File read default value set
	iRet = OBRSetDefaultSymbology();	// 1D(OBR) driver mode will be ini File vallue 
	iRet = OBRSetScanningKey(OBR_TRIGGERKEY_L|OBR_TRIGGERKEY_R|OBR_CENTERTRIGGER);
	iRet = OBRSetScanningCode(OBR_ALL);
	iRet = OBRSetBuffType(OBR_BUFOBR);	// 1D(OBR) driver mode will be OBR_BUFOBR
	iRet = OBROpen(m_hWnd, 0);			// OBRDRV open
	if(iRet == OBR_ERROR_INVALID_ACCESS)
	{
		ShowErrorMessage(IDS_MSG_CAMERAFAIL, IDS_APP_TITLE);
		return -1;
	}
	else if(iRet == OBR_ERROR_HOTKEY)
	{
		ShowErrorMessage(IDS_MSG_HOTKEYFAIL, IDS_APP_TITLE);
		return -1;
	}
	else if(iRet != OBR_OK){
		ShowErrorMessage(IDS_MSG_CONNECTFAIL, IDS_APP_TITLE);
		return -1;
	}

	StartScanChk(m_hWnd);
	::SetFocus(m_pBaseEdit1->GetSafeHwnd());
	return CFrameWnd::OnCreate( lpcs );
}


void CMainWnd::OnClose(void)
{
	int i;

	KillRxThread2 = 0;
	SetEvent(hObrStatus);
	KillRxThread1 = 1;
	SetEvent(hObrStatus);

	for(i = 0 ; i < 31 ; i++){
		// thread stop flag ON?
		if(KillRxThread2 == 1){
			WaitForSingleObject(hKillDispatchThread, 100);
			break ;
		}
		WaitForSingleObject(hKillDispatchThread, 100);
	}

	WaitForSingleObject(hObrStatus, 100);
	CloseHandle(ghWatchThread);	
	CloseHandle(hObrStatus);
	CloseHandle(hKillDispatchThread);
	OBRClose();		// OBRDRV Close
	DestroyWindow();
}


void CMainWnd::OnDestroy(void)
{
	CFrameWnd::OnDestroy();
	PostQuitMessage( 0 );
}


BOOL CMyApp::InitInstance()
{
	CString szTitle;
	CString szClass;
	HWND hWnd;
	WNDCLASS wc;
	DWORD dwStyle = WS_CAPTION | WS_SYSMENU;
	DWORD dwExStyle =  WS_EX_WINDOWEDGE;
	RECT rcBase = { 0, 0, 240, 320 - TASKBAR_HEIGHT};
	BOOL bRet;

	g_hInst = m_hInstance;

	szTitle.LoadString(IDS_APP_TITLE);
	szClass.LoadString(IDS_WND_CLASS);
	hWnd = FindWindow(WINCE_CLASSTAG + szClass, szTitle);
	if(hWnd){
		SetForegroundWindow((HWND)((ULONG)hWnd | 0x00000001));
		return FALSE;
	}

	CWinApp::InitInstance();

	wc.style			= CS_HREDRAW | CS_VREDRAW;
	wc.lpfnWndProc		= DefWindowProc;
	wc.cbClsExtra		= 0;
	wc.cbWndExtra		= 0;
	wc.hInstance		= m_hInstance;
	wc.hIcon			= NULL;
	wc.hCursor			= AfxGetApp()->LoadCursor(IDC_ARROW);
	wc.hbrBackground	= (HBRUSH)(COLOR_STATIC + 1);
	wc.lpszMenuName		= NULL;
	wc.lpszClassName	= (LPCTSTR)szClass;

	if(!AfxRegisterClass(&wc)){
		return FALSE;
	}

	m_pMainWnd = new CMainWnd;
	if(!m_pMainWnd){
		return FALSE;
	}

	CMainWnd *pBaseWnd = static_cast< CMainWnd* >( m_pMainWnd );
	bRet = pBaseWnd->Create(szClass, szTitle, dwStyle, rcBase, NULL, NULL, dwExStyle);
	if(!bRet || !m_pMainWnd->GetSafeHwnd()){
		return FALSE;
	}
	m_pMainWnd->ShowWindow(m_nCmdShow);
	m_pMainWnd->UpdateWindow();

	return TRUE;
}

CMyApp MyApp;
