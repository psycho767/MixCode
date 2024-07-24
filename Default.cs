using System;
using System.IO;

public partial class CaptureImages : System.Web.UI.Page
{
    public static int Random=0; public static string path;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            path = Server.MapPath("~\\") + "Images\\";
            txt_path.Text = path;
        }
           
        try
        {
            // Handle the HTTP POST request
            if (Request.HttpMethod == "POST")
            {
                // Read the JSON data from the request
                string json = new StreamReader(Request.InputStream).ReadToEnd();
                Console.WriteLine("Received JSON data: " + json);
                // Deserialize JSON to get the image data
                if (!string.IsNullOrEmpty(json))
                {
                    ImageDataModels imageData = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageDataModels>(json);

                    // Save the image data to a server folder
                    try
                    {
                        SaveImageToServer(imageData.ImageData);

                        // Respond to the client (optional)
                        Response.ContentType = "application/json";
                        Response.Write("{\"success\": true}");
                        Response.End();
                    }
                    catch (Exception ex)
                    {
                        // Log error
                        Console.WriteLine("Error saving image: " + ex.Message);

                        // Respond with an error (optional)
                        Response.ContentType = "application/json";
                        Response.Write("{\"success\": false, \"error\": \"" + ex.Message + "\"}");
                        Response.End();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
    }

    private void SaveImageToServer(string base64Data)
    {
        Random = Random + 1;
        // Decode and save the image data to a server folder
        byte[] bytes = Convert.FromBase64String(base64Data.Split(',')[1]);

       string  path1 = Server.MapPath("~") + "\\Images\\";
        string filePath = path1  + DateTime.Now.ToString("yyyyMMddHHmmssfff") +"-"+Random+ ".png";
        List.Items.Add(filePath);
        List.DataBind();
        File.WriteAllBytes(filePath, bytes);
    }
    
}



public class ImageDataModels
{
    public string ImageData { get; set; }
}
