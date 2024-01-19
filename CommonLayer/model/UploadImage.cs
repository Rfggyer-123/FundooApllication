using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using static System.Net.Mime.MediaTypeNames;

namespace CommonLayer.model
{
    public class UploadImage
    {
    //    public string UploadImageMethod(string filePath, String title, long userId)
    //    {



    //        Account account = new Account("dd2pal6jj", "715731728516283", "5WuFtsDfAMASxfCSGRTqZbE4lqE");
    //        Cloudinary cloudinary = new Cloudinary(account);




    //        var uploadParameters = new ImageUploadParams()
    //        {
    //            File = new FileDescription(Image.FileName, Image.OpenReadStream())
    //        };

    //        var uploadResult = cloudinary.Upload(uploadParameters);
    //        string ImagePath = uploadResult.Url.ToString();
    //        noteEntity.Image = ImagePath;
    //        notesContext.Entry(noteEntity).State = EntityState.Modified;
    //        notesContext.SaveChanges();
    //        return noteEntity;

    //    }


    //public string UploadImageMethod(string filePath, string title, long userId)
    //{
    //    try
    //    {
    //        filePath = "https://www.google.com/imgres?imgurl=https%3A%2F%2Frevsportz.in%2Fwp-content%2Fuploads%2F2023%2F04%2Fvirat-kohli-india-cricket-t20-world-cup_5940705-1024x576.jpg&tbnid=a7ZL3-Xl2PQceM&vet=12ahUKEwi3r-fvjsuDAxVXT2wGHUacAtkQMygCegQIARA2..i&imgrefurl=https%3A%2F%2Frevsportz.in%2Floving-what-you-do-at-the-heart-of-handling-pressure-kohli%2F&docid=Fju3ZBwkm1onIM&w=1024&h=576&q=virat%20kohli%20photo%20with%20less%20link%20address&ved=2ahUKEwi3r-fvjsuDAxVXT2wGHUacAtkQMygCegQIARA2";

    //        Account account = new Account("dd2pal6jj", "715731728516283", "5WuFtsDfAMASxfCSGRTqZbE4lqE");
    //        Cloudinary cloudinary = new Cloudinary(account);
    //        CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
    //        {
    //            File = new FileDescription(filePath),
    //            PublicId = title
    //        };

    //        CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

    //        if (uploadResult != null && uploadResult.Url != null)
    //        {
    //            var path = uploadResult.Url.ToString();
    //            // Additional processing if needed

    //            return path;
    //        }
    //        else
    //        {
    //            // Handle the case where uploadResult or uploadResult.Url is null
    //            return "Error: Image upload result or URL is null";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception for debugging purposes
    //        Console.WriteLine($"Exception: {ex.ToString()}");

    //        // Return an error message
    //        return $"Error: {ex.Message}";
    //    }
    //}


    //public string UploadImageMethod(string imageUrl, string title, long userId)
    //{
    //    try
    //    {
    //        byte[] imageBytes = null;

    //        // Download the image from the URL
    //        using (WebClient webClient = new WebClient())
    //        {
    //            try
    //            {
    //                imageBytes = webClient.DownloadData(imageUrl);
    //            }
    //            catch (Exception ex)
    //            {
    //                // Log or handle the download exception
    //                Console.WriteLine($"Error downloading image: {ex.ToString()}");
    //                return $"Error downloading image: {ex.Message}";
    //            }
    //        }

    //        if (imageBytes != null && imageBytes.Length > 0)
    //        {
    //            // Cloudinary setup
    //            Account account = new Account("dd2pal6jj", "715731728516283", "5WuFtsDfAMASxfCSGRTqZbE4lqE");
    //            Cloudinary cloudinary = new Cloudinary(account);

    //            // Upload the image to Cloudinary
    //            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
    //            {
    //                File = new FileDescription("temp.jpg", new MemoryStream(imageBytes)), // Use a temporary file name
    //                PublicId = title
    //            };

    //            try
    //            {
    //                // Upload the image to Cloudinary
    //                CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

    //                if (uploadResult != null && uploadResult.Url != null)
    //                {
    //                    var path = uploadResult.Url.ToString();
    //                    // Additional processing if needed
    //                    return path;
    //                }
    //                else
    //                {
    //                    // Log details about uploadResult
    //                    Console.WriteLine($"UploadResult: {uploadResult}");
    //                    // Handle the case where uploadResult or uploadResult.Url is null
    //                    return "Error: Image upload result or URL is null";
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                // Log or handle the Cloudinary upload exception
    //                Console.WriteLine($"Error uploading image to Cloudinary: {ex.ToString()}");
    //                return $"Error uploading image to Cloudinary: {ex.Message}";
    //            }
    //        }
    //        else
    //        {
    //            // Handle the case where imageBytes is null or empty
    //            return "Error: Image download failed or the downloaded image is empty.";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception for debugging purposes
    //        Console.WriteLine($"Exception: {ex.ToString()}");

    //        // Return a generic error message
    //        return "An unexpected error occurred.";
    //    }
    //}


  

    }
}

