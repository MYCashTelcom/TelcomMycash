using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class CRM_CRM_PHOTO_UPLOAD : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objSrvHandler = new clsServiceHandler();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
            }
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            } 
            btnChangePic.Visible = false;
            ShowPhoto("");            
        }
        try
        {
            strUserName = Session["UserLoginName"].ToString();
            strPassword = Session["Password"].ToString();
        }
        catch
        {
            Session.Clear();
            Response.Redirect("../frmSeesionExpMesage.aspx");
        }
        // Start - Check active session
        try
        {
            string sess_id = HttpContext.Current.Session.SessionID;
            string strSessID = objSysAdmin.GetActiveSess(sess_id, Session["UserID"].ToString());

            if (strSessID == Session["Sess_ID"].ToString())
            {
                Session.Timeout = Convert.ToInt32(Session["SessionOut"].ToString());
            }
            else
            {
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            Response.Redirect("../frmSeesionExpMesage.aspx");
        }
        // End - Check active session
    }     
    public void ShowPhoto( string strClientID)
    {
        try
        {
            imgClientPic.ImageUrl = "";
            imgClientSig.ImageUrl = "";
            imgCustomerKyc.ImageUrl = "";
            DataSet dsClientInfo = new DataSet();
            dsClientInfo.Tables.Clear();
           //dsClientInfo = objSrvHandler.GetPhotoSignature(ddlClient_list.SelectedValue.ToString());
            dsClientInfo = objSrvHandler.GetPhotoSignature(strClientID);
            if (dsClientInfo.Tables[0].Rows.Count > 0)
            {
                string Clientpicture = dsClientInfo.Tables[0].Rows[0]["CLIENT_PIC"].ToString();
                string ClientSignature = dsClientInfo.Tables[0].Rows[0]["SIGNATURE"].ToString();
                string ClientKyc = dsClientInfo.Tables[0].Rows[0]["KYC_FORM_PIC"].ToString();
                imgClientPic.ImageUrl = "~/ClientImages/" + Clientpicture;
                imgClientSig.ImageUrl = "~/ClientSignature/" + ClientSignature;
                imgCustomerKyc.ImageUrl = "~/ClientKyc/" + ClientKyc;
                btnSave.Visible = false;
                btnChangePic.Visible = true;
            }
            else
            {
                imgClientPic.ImageUrl = "";
                imgClientSig.ImageUrl = "";
                imgCustomerKyc.ImageUrl = "";
                btnSave.Visible = true;
                btnChangePic.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string picture_name = string.Empty;
        string Signature_name = string.Empty;
        string kyc_form_name = string.Empty;
        string strClientID = "", strClientIDPic = ""; 
        string strDirectorID="";

        //############## check client photo and signature size #############################
        if (fluClientPic.HasFile)
        {
            int fileSize = fluClientPic.PostedFile.ContentLength;
            //Limit size to approx 100kb for image
            if ((fileSize > 0 & fileSize > 102400))
            {
                lblMessage.Text = "Picture size must be less than 100kb.";
                return;
            }
        }
        if (fluClientSig.HasFile)
        {
            int fileSize = fluClientSig.PostedFile.ContentLength;
            //Limit size to approx 100kb for image
            if ((fileSize > 0 & fileSize > 102400))
            {
                lblMessage.Text = "Signature size must be less than 100kb.";
                return;
            }
        }
        if (fluCustomerKyc.HasFile)
        {
            int fileSize = fluCustomerKyc.PostedFile.ContentLength;
            // limit size to appriximate 300kb for image
            if ((fileSize > 0 & fileSize > (102400*3)))
            {
                lblMessage.Text = "Customer KYC Form size must be less than 100kb.";
                return;
            }
        }


        //#############################################################################
        strClientIDPic = objSrvHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "CLINT_ID", "ACCNT_NO", txtAccountNo.Text.Trim());
        if (fluClientPic.HasFile)
        {
            picture_name = strClientIDPic; //ddlClient_list.SelectedValue.ToString();
            strClientID = strClientIDPic; //ddlClient_list.SelectedValue.ToString();
            string ActualFileName1 = fluClientPic.FileName;
            picture_name = picture_name + System.IO.Path.GetExtension(ActualFileName1);
            System.Drawing.Image newImage1 = System.Drawing.Image.FromStream((fluClientPic).PostedFile.InputStream);
            fluClientPic.SaveAs(Server.MapPath("~/ClientImages/") + picture_name);
        }
        if (picture_name == "") picture_name = "no_image.jpg";
        //#################################Signature########################
        if (fluClientSig.HasFile)
        {
            Signature_name = strClientIDPic; //ddlClient_list.SelectedValue.ToString();
            strClientID = strClientIDPic;//ddlClient_list.SelectedValue.ToString();
            string ActualFileName1 = fluClientSig.FileName;
            Signature_name = Signature_name + System.IO.Path.GetExtension(ActualFileName1);
            System.Drawing.Image newImage1 = System.Drawing.Image.FromStream((fluClientSig).PostedFile.InputStream);
            fluClientSig.SaveAs(Server.MapPath("~/ClientSignature/") + Signature_name);
        }
        if (Signature_name == "") Signature_name = "no_image.jpg";

        // ######################## FOR KYC FORM ###############################
        if (fluCustomerKyc.HasFile)
        {
            kyc_form_name = strClientIDPic;
            strClientID = strClientIDPic;
            string ActualFileName1 = fluCustomerKyc.FileName;
            kyc_form_name = kyc_form_name + System.IO.Path.GetExtension(ActualFileName1);
            System.Drawing.Image newImage1 = System.Drawing.Image.FromStream((fluCustomerKyc).PostedFile.InputStream);
            fluCustomerKyc.SaveAs(Server.MapPath("~/ClientKyc/") + kyc_form_name);

            if (kyc_form_name == "") kyc_form_name = "no_image.jpg";
        }


        string strResult = objSrvHandler.InsertPhotoSignature(strClientID, picture_name, Signature_name, kyc_form_name, strDirectorID);
       
        if (strResult != null)
        {
            lblMessage.Text = "Client Photo & Signature Inserted Successfully";
            SaveAuditInfo("Insert", "Client Photo Signature");
        }
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        //objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void btnChangePic_Click(object sender, EventArgs e)
    {
        string picture_name = string.Empty; ;
        string Signature_name = string.Empty;
        string kyc_pic_name = string.Empty;
        string strPhotoID = "", strClientIDPic="";
        //############## check client photo and signature size #############################
        if (fluClientPic.HasFile)
        {
            int fileSize = fluClientPic.PostedFile.ContentLength;
            //Limit size to approx 100kb for image
            if ((fileSize > 0 & fileSize > 102400))
            {
                lblMessage.Text = "Picture size must be less than 100kb.";
                return;
            }
        }
        if (fluClientSig.HasFile)
        {
            int fileSize = fluClientSig.PostedFile.ContentLength;
            //Limit size to approx 100kb for image
            if ((fileSize > 0 & fileSize > 102400))
            {
                lblMessage.Text = "Signature size must be less than 100kb.";
                return;
            }
        }
        if (fluCustomerKyc.HasFile)
        {
            int fileSize = fluCustomerKyc.PostedFile.ContentLength;
            //Limit size to approx 100kb for image
            if ((fileSize > 0 & fileSize > (102400*3)))
            {
                lblMessage.Text = "Kyc form size must be less than 300kb.";
                return;
            } 
        }



        //##########################################################################
        DataSet odsDataset = new DataSet();
        odsDataset.Tables.Clear();
        strClientIDPic = objSrvHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "CLINT_ID", "ACCNT_NO", txtAccountNo.Text.Trim());
        try
        {
            strPhotoID = strClientIDPic; //ddlClient_list.SelectedValue.ToString();
            odsDataset = objSrvHandler.GetPhotoSignature(strPhotoID);
            if (odsDataset.Tables[0].Rows[0]["CLIENT_PIC"].ToString() != "" || odsDataset.Tables[0].Rows[0]["SIGNATURE"].ToString() != ""
                        || odsDataset.Tables[0].Rows[0]["KYC_FORM_PIC"].ToString() != "")
            {
                if (fluClientPic.HasFile)
                {
                    picture_name = strPhotoID;
                    string ActualFileName1 = fluClientPic.FileName;
                    picture_name = picture_name + System.IO.Path.GetExtension(ActualFileName1);
                    System.Drawing.Image newImage1 = System.Drawing.Image.FromStream((fluClientPic).PostedFile.InputStream);
                    fluClientPic.SaveAs(Server.MapPath("~/ClientImages/") + picture_name);
                }
                if (fluClientPic.PostedFile.ContentLength == 0) picture_name = odsDataset.Tables[0].Rows[0]["CLIENT_PIC"].ToString();
                //#################################Signature########################
                if (fluClientSig.HasFile)
                {
                    Signature_name = strPhotoID;
                    string ActualFileName1 = fluClientSig.FileName;
                    Signature_name = Signature_name + System.IO.Path.GetExtension(ActualFileName1);
                    System.Drawing.Image newImage1 = System.Drawing.Image.FromStream((fluClientSig).PostedFile.InputStream);
                    fluClientSig.SaveAs(Server.MapPath("~/ClientSignature/") + Signature_name);
                }
                if (fluClientSig.PostedFile.ContentLength == 0) Signature_name = odsDataset.Tables[0].Rows[0]["SIGNATURE"].ToString();

                // #################### for kyc form upload  ###############################
                if (fluCustomerKyc.HasFile)
                {
                    kyc_pic_name = strPhotoID;
                    string ActualFileName1 = fluCustomerKyc.FileName;
                    kyc_pic_name = kyc_pic_name + System.IO.Path.GetExtension(ActualFileName1);
                    System.Drawing.Image newImage1 = System.Drawing.Image.FromStream((fluCustomerKyc).PostedFile.InputStream);
                    fluCustomerKyc.SaveAs(Server.MapPath("~/ClientKyc/") + kyc_pic_name);
                }
                if (fluCustomerKyc.PostedFile.ContentLength == 0) kyc_pic_name = odsDataset.Tables[0].Rows[0]["KYC_FORM_PIC"].ToString();
                
                
                
                
                string strResult = objSrvHandler.UpdatePhotoSignature(strPhotoID, picture_name, Signature_name, kyc_pic_name);
                if (strResult != null)
                {
                    lblMessage.Text = "Client Photo & Signature Updated Successfully";
                    SaveAuditInfo("Update", "Client Photo Signature");
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
       //ddlClient_list.ClearSelection();
        lblMessage.Text = "";
        string strClientID="";
        try
        {
            string strMobileNo = "+88" + (txtAccountNo.Text.Trim()).Substring(0, 11);
            strClientID = objSrvHandler.ReturnOneColumnValueByAnotherColumnValue("ACCOUNT_LIST", "CLINT_ID", "ACCNT_NO", txtAccountNo.Text.Trim());
            //ddlClient_list.Items.FindByText(strMobileNo).Selected = true;
            ShowPhoto(strClientID);
        }
        catch (Exception exx)
        {
            lblMessage.Text = exx.Message.ToString();
        }
    }
    //override protected void OnInit(EventArgs e)
    //{
    //    btnSearch.Attributes.Add("onclick", "javascript:" +
    //              btnSearch.ClientID + ".disabled=true;" +
    //              this.GetPostBackEventReference(btnSearch));
    //    InitializeComponent();
    //    base.OnInit(e);
    //}

    //private void InitializeComponent()
    //{
    //    this.btnSearch.Click +=
    //            new System.EventHandler(this.btnSearch_Click);
    //    this.Load += new System.EventHandler(this.Page_Load);
    //}
    //protected void btnView_Click(object sender, EventArgs e)
    //{
    //    ShowPhoto("");
    //}
}
