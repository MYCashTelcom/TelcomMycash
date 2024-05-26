using System;
using System.Data;
using System.Configuration;

using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Forms_frmWellcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (TreeView1.SelectedValue.Equals("sysug"))
        {
            Iframe1.Attributes["src"] = "frmSysUsrGroup.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("sysErrLog"))
        {
            Iframe1.Attributes["src"] = "frmQuerySystemError.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("backgp"))
        {
            Iframe1.Attributes["src"] = "frmCheckBacgroundProcess.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("olSQL_Window"))
        {
            Iframe1.Attributes["src"] = "frmSQL_Terminal.aspx";
        }            
        ///------ Manage Service & Package -----------------
        else if (TreeView1.SelectedValue.Equals("mngSrvType"))
        {
            Iframe1.Attributes["src"] = "frmMngServiceType.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("mngService"))
        {
            Iframe1.Attributes["src"] = "frmMngService.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("mngSrvRate"))
        {
            Iframe1.Attributes["src"] = "frmMngSrvRate.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("mngPackage"))
        {
            Iframe1.Attributes["src"] = "frmMngPackage.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("mngSecQus"))
        {
            Iframe1.Attributes["src"] = "frmMBSecretQuestions.aspx";
        }
        ///-------------------------------------------------------
        ///---------------- Manage Clients & Accounts -----------
        else if (TreeView1.SelectedValue.Equals("acchld"))
        {
            Iframe1.Attributes["src"] = "frmMngCilentList.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("acqr"))
        {
            Iframe1.Attributes["src"] = "frmMngClientAccount.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("acActive"))
        {
            Iframe1.Attributes["src"] = "frmAccountActivation.aspx";
        }    
        else if (TreeView1.SelectedValue.Equals("mngBulkAcc"))
        {
            Iframe1.Attributes["src"] = "frmBulkClientCreation.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("mngPIN"))
        {
            Iframe1.Attributes["src"] = "frmManageAccntPIN.aspx";
        }
        ///--------------------------------------------------------
        ///--------- Manage Group Account -----------------------
        else if (TreeView1.SelectedValue.Equals("mngAccntGroup"))
        {
            Iframe1.Attributes["src"] = "frmMngGroupAccount.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("mngAccntGroupMember"))
        {
            Iframe1.Attributes["src"] = "frmMngGroupAccntMembers.aspx";
        }
        ///--------------------------------------------------------
        ///--------- Manage Messages -----------------------
        else if (TreeView1.SelectedValue.Equals("mng_quiz"))
        {
            Iframe1.Attributes["src"] = "frmQuizList.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("sbtGrpMsg"))
        {
            Iframe1.Attributes["src"] = "frmMngGroupSMS_Submit.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("sndBrdMessage"))
        {
            Iframe1.Attributes["src"] = "frmBroadcastMSG.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("qrymsg"))
        {
            Iframe1.Attributes["src"] = "frmQuerySubmitStatus.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("qryInvStatus"))
        {
            Iframe1.Attributes["src"] = "frmQueryInvitationStatus.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("qryQuzResult"))
        {
            Iframe1.Attributes["src"] = "frmQueryQuizResult.aspx";
        }    
        ///-------------------------------------------------------  
        /// ---------Manage Content ------------------------------
        else if (TreeView1.SelectedValue.Equals("cnttype"))
        {
                    Iframe1.Attributes["src"] = "frmContentType.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("cntgrp"))
        {
            Iframe1.Attributes["src"] = "frmContentGroup.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("cntlist"))
        {
            Iframe1.Attributes["src"] = "frmContentList.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("cnttxtedit"))
        {
            Iframe1.Attributes["src"] = "frmContentTextEdit.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("cntusdhis"))
        {
            Iframe1.Attributes["src"] = "frmContentHistory.aspx";
        }  
        /// ------------------------------------------------------
        ///--------- Manage Voucher-------------------------------
        else if (TreeView1.SelectedValue.Equals("vouDenomination"))
        {
                    Iframe1.Attributes["src"] = "frmVouDenomination.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("vouGeneration"))
        {
                    Iframe1.Attributes["src"] = "frmVoucherGeneration.aspx";
        }
        ///--------------------------------------------------------
        ///-------------------------------------------------------  
        ///--------- Manage Mobile Banking-------------------------------
        else if (TreeView1.SelectedValue.Equals("mngBankList"))
        {
            Iframe1.Attributes["src"] = "frmMngBankList.aspx";
        }        
        else if (TreeView1.SelectedValue.Equals("mngCBAccount"))
        {
            Iframe1.Attributes["src"] = "frmMngClintBankAccount.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("bnkTranHis"))
        {
            Iframe1.Attributes["src"] = "frmQueryBankTransaction.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("bnkSettlement"))
        {
            Iframe1.Attributes["src"] = "frmQueryBankSatlementTran.aspx";
        }
            
        ///-------------------------------------------------------  
        ///--------- Manage Mobile Cash-------------------------------
        ///----------------------------------------------------------------
        ///------------------ Mobile Zone ---------------------------------
        else if (TreeView1.SelectedValue.Equals("zoarea"))
        {
            Iframe1.Attributes["src"] = "frmMZoneCoverageArea.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("clsts"))
        {
            Iframe1.Attributes["src"] = "frmMZoneCellSites.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("mngzone"))
        {
            Iframe1.Attributes["src"] = "frmMZoneList.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("mngzonecell"))
        {
            Iframe1.Attributes["src"] = "frmMZoneZoneCellSites.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("mngcbm"))
        {
            Iframe1.Attributes["src"] = "frmMZoneCB_Message.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("mngznpkg"))
        {
            Iframe1.Attributes["src"] = "frmMZonePackages.aspx";
        }
        ///------------------------------------------------------------------
        ///----------------- Manage APSNG-----------------------------------
        else if (TreeView1.SelectedValue.Equals("mngIN_Pkg"))
        {
            Iframe1.Attributes["src"] = "frmAPSNG_IN_Packages.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("pkgChngRule"))
        {
            Iframe1.Attributes["src"] = "frmAPSNG_Pkg_Change_Rule.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("pkgChngBonus"))
        {
            Iframe1.Attributes["src"] = "frmAPSNG_PKG_ChangeBonus.aspx";
        }
        else if (TreeView1.SelectedValue.Equals("pkgChngMsg"))
        {
            Iframe1.Attributes["src"] = "frmAPSNG_PKG_Change_Message.aspx";
        }
        ///------------------------------------------------------------------
        ///
        else if (TreeView1.SelectedValue.Equals("rptList"))
        {
            Iframe1.Attributes["src"] = "frmReportList.aspx";
        }
        
        else
        {
        }
    }
}
