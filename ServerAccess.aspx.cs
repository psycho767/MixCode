using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class ServerAccess : System.Web.UI.Page
{
    private static string sess_emp_no = "klasd@#$jhaj#23^*nnj*^^jhasd@!#$$%ajskldfvn#$$k#j%jbh^b^bv&^bv&bv%b$n$bv$";
    private static string ip = "klasd@#$jhaj#23^*nnj*^^jhasd@!#$$%ajskldfvn#$$k#j%jbh^b^bv&^bv&bv%b$n$bv$";
    Boolean IsAllowed = false;
    protected void Page_Load(object sender, EventArgs e)
    {

        string config_empid = System.Configuration.ConfigurationManager.AppSettings["EmployeeId"].ToString();
        string config_systemip = System.Configuration.ConfigurationManager.AppSettings["SystemIp"].ToString();

        string ip = Request.ServerVariables["REMOTE_ADDR"];
        if (Session["EmployeeNo"] == null)
            sess_emp_no = "1000"; // Session["EmployeeNo"].ToString();

        if (config_empid.Contains(sess_emp_no) == true && config_systemip.Contains(ip) == true)
        {
            IsAllowed = true;
            if (!IsPostBack)
            {
                string mapPath = Server.MapPath("~");
                getAllDiretories(mapPath);
                getAllFiles(mapPath);
                txt_sear.Text = mapPath;
                txt_navbar.Text = mapPath;
                btn_download.Visible = false;
                NavUrl();
            }
        }
        else
        {
           // Response.Redirect("login.aspx");
        }
    }
    protected void txt_sear_TextChange(object sender, EventArgs e)
    {
        string mapPath;
        mapPath = txt_sear.Text;
        getAllDiretories(mapPath);
        getAllFiles(mapPath);

        txt_navbar.Text = mapPath;
        NavUrl();
    }
    protected void txt_navbar_SelectedChanged(object sender, EventArgs e)
    {       // if (txt_sear.Text.LastIndexOf("\\") == txt_sear.Text.Length-1)
        string mapPath;
        mapPath = txt_navbar.Text;
        getAllDiretories(mapPath);
        getAllFiles(mapPath);
        txt_sear.Text = mapPath;
    }
    public void getAllDiretories(string mapPath)
    {
        DataTable dt = new DataTable();
        dt.Rows.Clear();
        dt.Columns.Add("ServerDir");
        try
        {
            string[] directories = Directory.GetDirectories(mapPath);
            foreach (string directory in directories)
            {//listBox1.Items.Add(Path.GetFileName(directory));
                string test = Path.GetFileName(directory);
                dt.Rows.Add(test);
            }
            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            Folder_Repeater.DataSource = dt;
            Folder_Repeater.DataBind();
        }
        catch (Exception ex)
        {
            lbl_msg1.Text = "Dir Error....." + " " + ex.Message.ToString();
        }
    }
    public void getAllFiles(string mapPath)
    {
        DataTable dt1 = new DataTable();
        dt1.Rows.Clear();
        dt1.Columns.Add("ServerDir");
        dt1.Columns.Add("image_url");
        DataRow dr;
        try
        {
            string[] Files = Directory.GetFiles(mapPath);
            foreach (string files in Files)
            {
                dr = dt1.NewRow();
                string test = Path.GetFileName(files);
                dr[0] = (test);
                string ext = Path.GetExtension(files);
                if (ext == ".aspx")
                {
                    dr[1] = ("images/designAspx.png");
                }
                else if (ext == ".cs")
                {
                    dr[1] = ("images/backAspx.png");
                }
                else if (ext == ".vb")
                {
                    dr[1] = "images/vbbackAspx.png";
                }
                else if (ext == ".pdf")
                {
                    dr[1] = "images/pdfImage.png";
                }
                else
                { dr[1] = ("images/rxx.jpg"); }


                dt1.Rows.Add(dr);
            }
            Repeater2.DataSource = dt1;
            Repeater2.DataBind();

            Files_Repeater.DataSource = dt1;
            Files_Repeater.DataBind();
        }
        catch (Exception ex)
        {
            lbl_msg1.Text = "Fil Error....." + " " + ex.Message.ToString();
        }
    }
    protected void btn_download_Click(object sender, EventArgs e)
    {
        string selectedPath = txt_filepath.Text;
        downloadFiles(selectedPath);
    }
    public void downloadFiles(string selectedPath)
    {
        try
        {
            string FilePath = (selectedPath);
            string fileName = Path.GetFileName(FilePath);
            if (File.Exists(FilePath))
            {
                System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                Response.Clear();
                Response.ClearContent();
                Response.AppendHeader("Content-Disposition", "attachment; filename =" + fileName);
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(FilePath);
                Response.Flush();
                Response.End();
            }
            else
            {
                lbl_msg1.Text = "Error";
            }
        }
        catch (Exception ex)
        {
            lbl_msg1.Text = ex.Message.ToString();
        }
    }
    protected void Dir_Click(object sender, EventArgs e)
    {
        string folderName = (sender as LinkButton).CommandArgument.ToString();
        prev_link.Value = txt_sear.Text;


        string NewPath = txt_sear.Text + "\\" + folderName;
        txt_sear.Text = NewPath;
        txt_navbar.Text = NewPath;
        getAllDiretories(NewPath);
        getAllFiles(NewPath);
        NavUrl();
    }
    protected void btn_pre_Click(object sender, ImageClickEventArgs e)
    {
        string prelink;
        string path = txt_sear.Text;
        if (path.LastIndexOf("\\") > -1)
            prelink = path.Substring(0, path.LastIndexOf("\\"));
        else
            prelink = path;
        txt_sear.Text = prelink;
        txt_navbar.Text = prelink;
        string NewPath = prelink;
        getAllDiretories(NewPath);
        getAllFiles(NewPath);
    }
    protected void btn_sear_Click(object sender, ImageClickEventArgs e)
    {
        prev_link.Value = txt_sear.Text;
        string NewPath = txt_sear.Text;

        getAllDiretories(NewPath);
        getAllFiles(NewPath);
    }
    protected void lnl_files_Click(object sender, ImageClickEventArgs e)
    {
        string fileName = (sender as ImageButton).CommandArgument.ToString();
        txt_filepath.Text = txt_sear.Text + "\\" + fileName;
        txt_navbar.Text = txt_sear.Text + "\\" + fileName;
    }
    protected void Files_Repeater_ItemCommand(object source, RepeaterCommandEventArgs e) { }

    protected void files_imgbtn_Click(object sender, ImageClickEventArgs e)
    {
        string filesName = (sender as ImageButton).CommandArgument.ToString();
        string selectedPath = txt_sear.Text + "\\" + filesName;
        string ServerPath = selectedPath;
        downloadFiles(ServerPath);
    }
    protected void folder_Click(object sender, ImageClickEventArgs e)
    {
        string folderName = (sender as ImageButton).CommandArgument.ToString();
        prev_link.Value = txt_sear.Text;
        string NewPath;
        if (txt_sear.Text.LastIndexOf("\\") == txt_sear.Text.Length - 1)
            NewPath = txt_sear.Text + folderName;
        else
            NewPath = txt_sear.Text + "\\" + folderName;
        txt_sear.Text = NewPath;
        txt_navbar.Text = NewPath;
        getAllDiretories(NewPath);
        getAllFiles(NewPath);
        NavUrl();
    }

    protected void Upload_Click(object sender, EventArgs e)
    {
        popupBox.Style.Add("display", "block");
        txt_filepath.Text = txt_sear.Text;
        lbl_msg1.Text = "";
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        popupBox.Style.Add("display", "none");
        btn_Upload.Visible = true;
        FileUpload1.Visible = true;
        btn_download.Visible = false;
        popup_title.Text = "Upload Server File";
        lbl_dir.Visible = false;
        dir_Name.Visible = false;
    }

    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        try
        {
            string filepath, fileNames;
            if (IsAllowed == true)
            {

                if (FileUpload1.HasFile)
                {
                    if (txt_filepath.Text != "")
                    {
                        filepath = txt_filepath.Text + "\\";
                    }
                    else
                    {
                        filepath = txt_filepath.Text;
                    }

                    fileNames = FileUpload1.PostedFile.FileName;

                    int affRow = insertLog(filepath, fileNames);
                    if (affRow > 0)
                    {
                        //FileUpload1.PostedFile.SaveAs(Server.MapPath("~") + "\\" + filepath + fileNames);   //it takes server path to upload files and only on within a server
                        FileUpload1.PostedFile.SaveAs(filepath + fileNames);
                        lbl_msg1.Text = "File Uploaded Successfully......";
                        getAllDiretories(txt_navbar.Text);
                        getAllFiles(txt_navbar.Text);
                    }
                    else
                    {
                        lbl_msg1.Text = "Log Insertion Error or File not Uploaded......";
                    }

                }
                else
                {
                    lbl_msg1.Text = "Please Select File to Upload......";
                }
            }
        }
        catch (Exception ex)
        {
            lbl_msg1.Text = ex.Message.ToString();
        }
    }

    public int insertLog(string filePath, string fileNames)
    {
        string Sys_IP = Request.ServerVariables["REMOTE_ADDR"];
        string dateTime = System.DateTime.Now.ToString("MM/dd/yyy HH:mm:ss");
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["GenSoftwareConnectionString"].ConnectionString.ToString());
        SqlCommand cmd = new SqlCommand("insert into DotNetServerLogReport (file_path,file_name,uploaded_date,emp_no,emp_name,system_ip) values ('" + filePath + "','" + fileNames + "','" + dateTime + "','" + Session["EmployeeNo"] + "','" + Session["EmployeeName"] + "','" + Sys_IP + "')", con);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        return i;

    }

    protected void Create_Dir_Click(object sender, EventArgs e)
    {

        try
        {
            txt_filepath.Text = txt_navbar.Text;
            btn_Upload.Visible = false;
            FileUpload1.Visible = false;
            btn_download.Visible = true;
            popup_title.Text = "Create Directory";
            popupBox.Style.Add("display", "block");
            dir_Name.Visible = true;
            lbl_dir.Visible = true;
            dir_Name.Text = "";
            lbl_msg1.Text = "";
        }
        catch (Exception ex)
        {
            lbl_msg1.Text = ex.Message.ToString();
        }

    }


    protected void btn_Create_Dir_Click(object sender, EventArgs e)
    {
        create();
    }

    public void create()
    {
        string path = "";
        if (IsAllowed == true)
        {
            Boolean isExists = false;
            try
            {
                if (txt_filepath.Text == "")
                {
                    //path = "\\" + dir_Name.Text;
                    path = "\\" + txt_filepath.Text + "\\" + dir_Name.Text;

                }
                else
                {
                    path = txt_filepath.Text + "\\" + dir_Name.Text;
                }
                if (Directory.Exists(path) == false)   // it checks that directoty already exists or not
                {

                    Directory.CreateDirectory(path);
                    isExists = Directory.Exists(path);  // it check that directory has been created or not
                    if (isExists == true)
                    {
                        lbl_msg1.Text = "Directory Successfully Created......";
                        dir_Name.Text = "";
                        getAllDiretories(txt_navbar.Text);
                        getAllFiles(txt_navbar.Text);

                    }
                }
                else
                {
                    lbl_msg1.Text = "Directory Already Exists.......";
                }
            }
            catch (Exception ex)
            {
                lbl_msg1.Text = ex.Message.ToString();
            }
        }
    }




    protected void Test_Click(object sender, EventArgs e)
    {

        //string path = txt_navbar.Text + "\\psycho";
        //if (Directory.Exists(path) == true)   // it checks that directoty already exists or not
        //{
        //   string test =  Directory.GetDirectoryRoot(path);
        //    //Directory.CreateDirectory(txt_navbar.Text+"\\Psycho1");
        //}
    }

    public void createListFromString(string configEmpList1)
    {
        // string configEmpList = "11111,22222,33333,44444,55555" + ",";

        //string SystemIP = ;

        //List<string> NewEmpList = new List<string> { };
        //while (configEmpList.Contains(","))
        //{

        //    string sub_empno = configEmpList.Substring(0, configEmpList.IndexOf(","));
        //    NewEmpList.Add(sub_empno);
        //    configEmpList = configEmpList.Substring(configEmpList.IndexOf(",") + 1, configEmpList.Length - (configEmpList.IndexOf(",") + 1));
        //}

    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        sess_emp_no = "klasd@#$jhaj#23^*nnj*^^jhasd@!#$$%ajskldfvn#$$k#j%jbh^b^bv&^bv&bv%b$n$bv$";
        ip = "klasd@#$jhaj#23^*nnj*^^jhasd@!#$$%ajskldfvn#$$k#j%jbh^b^bv&^bv&bv%b$n$bv$";
        Session["EmployeeNo"] = null;
        Session["EmployeeName"] = null;
        Session["EmployeeNameHindi"] = null;
        Session["SectionHindi"] = null;
        Session["role"] = null;

        Session["Designation"] = null;
        Session["Section"] = null;
        IsAllowed = false;
        Response.Redirect("login.aspx");

    }

    public void NavUrl()
    {
        string substr, substr1 = "";
        string URL;
        string navUrl = txt_navbar.Text;
        int lastIndex = navUrl.LastIndexOf("\\");
        int lengthofUrl = navUrl.Length - 1;
        if (lastIndex == lengthofUrl)
            URL = navUrl;
        else
            URL = navUrl + "\\";
        DataTable dt = new DataTable();
        dt.Columns.Add("url_list");
        dt.Columns.Add("url");
        DataRow dr;

        while (URL.Contains("\\"))
        {
            dr = dt.NewRow();
            substr = URL.Substring(0, URL.IndexOf("\\"));
            dr[0] = (substr);

            if (substr1 == "")
            {
                substr1 = substr;
            }
            else
            {
                substr1 = substr1 + "\\" + substr;
            }
            dr[1] = substr1;
            URL = URL.Substring(URL.IndexOf("\\") + 1, URL.Length - (URL.IndexOf("\\") + 1));
            dt.Rows.Add(dr);
        }


        Repeater_Nav.DataSource = dt;
        Repeater_Nav.DataBind();

    }

    protected void nav_btn_click(object sender, EventArgs e)
    {
        string NewPath = (sender as Button).CommandArgument.ToString();
        txt_sear.Text = NewPath;
        txt_navbar.Text = NewPath;
        getAllDiretories(NewPath);
        getAllFiles(NewPath);
        NavUrl();
    }

}