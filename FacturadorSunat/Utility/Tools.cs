using System.Xml.Linq;

namespace FacturadorSunat.Utility;

public static class Tools
{
    public static String BuildComplexTagPrefix(List<String> tags)
    {
        if (tags == null || (tags != null && tags.Count == 0))
        {
            return String.Empty;
        }
        
        String finalTag = String.Empty;
        foreach(String item in tags!)
        {
            String concatenatedItem = "/" + item;
            finalTag += concatenatedItem;
        }

        return finalTag;
    }
}