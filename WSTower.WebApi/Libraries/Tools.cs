using System;
using System.Drawing;
using System.IO;

namespace WSTower.WebApi.Libraries
{
    public class Tools
    {
        public static Image ToImage(byte[] arr)
        {
            Image returnImage = null;
            using (MemoryStream ms = new MemoryStream(arr))
            {
                returnImage = Image.FromStream(ms);
            }
            return returnImage;
        }

        public static int GetAge(DateTime birthDate)
        {
            var today = DateTime.Today;

            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
