using System.Xml;
using System.Xml.Serialization;
using FacturadorSunat.Domain;
using FacturadorSunat.Domain.XmlEntities.XmlInvoice;
using System.Text;

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

    public static String ValidateStringIsNullOrEmpty(String stringValue)
    {
        return stringValue = String.IsNullOrEmpty(stringValue) ? "NoData" : stringValue;
    }

    public static String XMLSerializer(XmlTagsInvoice xmlData)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(XmlTagsInvoice));
        
        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        namespaces.Add("ext", XmlTagsNamespace.Ext);
        namespaces.Add("ds", XmlTagsNamespace.Ds);
        namespaces.Add("sac", XmlTagsNamespace.Sac);
        namespaces.Add("cbc", XmlTagsNamespace.Cbc);

        XmlWriterSettings writerSettings = new XmlWriterSettings()
        {
            Encoding = Encoding.UTF8,
            Indent = true,
            OmitXmlDeclaration = true
        };

        using StringWriter stringWriter = new StringWriter();
        using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, writerSettings);

        serializer.Serialize(xmlWriter, xmlData, namespaces);
        return stringWriter.ToString();
    }
}