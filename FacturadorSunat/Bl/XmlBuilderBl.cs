using System.Xml.Linq;

namespace FacturadorSunat.Bl;

public class XmlBuilderBl
{
   private static XmlBuilderBl instance = null;
   public static XmlBuilderBl Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new XmlBuilderBl();
            }
            return instance;
        }
    }

    public String

}