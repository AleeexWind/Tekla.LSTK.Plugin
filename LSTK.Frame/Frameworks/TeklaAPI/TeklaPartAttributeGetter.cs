using LSTK.Frame.Entities;
using System.Linq;
using Tekla.Structures.Catalogs;

namespace LSTK.Frame.Frameworks.TeklaAPI
{
    public static class TeklaPartAttributeGetter
    {
        public static bool GetProfileHeight(ElementData elementData, string profile)
        {
            try
            {
                LibraryProfileItem libraryProfileItem = new LibraryProfileItem();
                if(libraryProfileItem.Select(profile))
                {
                    string propertyName = "HEIGHT";
                    double profileHeight = libraryProfileItem.aProfileItemParameters.ToArray().Select(x => x as ProfileItemParameter).FirstOrDefault(t => t.Property.Equals(propertyName)).Value;

                    elementData.ProfileHeight = profileHeight;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public static double GetProfileHeight(string profile)
        {
            double result = 0;
            try
            {
                LibraryProfileItem libraryProfileItem = new LibraryProfileItem();
                if (libraryProfileItem.Select(profile))
                {
                    string propertyName = "HEIGHT";
                    double profileHeight = libraryProfileItem.aProfileItemParameters.ToArray().Select(x => x as ProfileItemParameter).FirstOrDefault(t => t.Property.Equals(propertyName)).Value;

                    result = profileHeight;
                }
            }
            catch
            {

            }
            return result;
        }
    }
}
