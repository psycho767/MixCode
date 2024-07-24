 using System;
using System.IO;

public partial class SaveImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Handle the HTTP POST request
        if (Request.HttpMethod == "POST")
        {
            // Read the JSON data from the request
            string json = new StreamReader(Request.InputStream).ReadToEnd();

            // Deserialize JSON to get the image data
            ImageDataModel imageData = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageDataModel>(json);

            // Save the image data to a server folder
            SaveImageToServer(imageData.ImageData);

            // Respond to the client (optional)
            Response.ContentType = "application/json";
            Response.Write("{\"success\": true}");
            Response.End();
        }
    }

    private void SaveImageToServer(string base64Data)
    {
        // Decode and save the image data to a server folder
        byte[] bytes = Convert.FromBase64String(base64Data.Split(',')[1]);
        string filePath = Server.MapPath("Images/image.png");
        File.WriteAllBytes(filePath, bytes);
    }
}

public class ImageDataModel
{
    public string ImageData { get; set; }
}
 