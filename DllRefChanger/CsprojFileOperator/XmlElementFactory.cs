﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DllRefChanger.CsprojFileOperator
{
    static class XmlElementFactory
    {
        public static XElement CreateProjectReferenceNode(string projFilePath, string projGuid, string projName, string namespaceName)
        {
            XElement xElement = new XElement(XName.Get("ProjectReference", namespaceName));
            xElement.SetAttributeValue(XName.Get("Include"), projFilePath);
            xElement.SetElementValue(XName.Get("Project", namespaceName), projGuid);
            xElement.SetElementValue(XName.Get("Name", namespaceName), projName);
            return xElement;
        }

        public static XElement CreateProjectReferenceNode(string projFilePath, string namespaceName)
        {
            XElement xElement = new XElement(XName.Get("ProjectReference", namespaceName));
            xElement.SetAttributeValue(XName.Get("Include"), projFilePath);
            return xElement;
        }

        public static XElement CreateItemGroupForReplaceReference(string projFilePath, string projGuid, string projName, string namespaceName)
        {
            XElement xElement = new XElement(XName.Get("ItemGroup",namespaceName));

            XElement removeReference = new XElement(XName.Get("Reference",namespaceName));
            removeReference.SetAttributeValue(XName.Get("Remove"), projName);

            XElement removePackageReference = new XElement(XName.Get("PackageReference", namespaceName));
            removePackageReference.SetAttributeValue(XName.Get("Remove"), projName);

            XElement insertProjcetReference =
                CreateProjectReferenceNode(projFilePath, projGuid, projName, namespaceName);

            xElement.Add(removeReference);
            xElement.Add(removePackageReference);
            xElement.Add(insertProjcetReference);

            return xElement;
        }

        public static XElement ChangeReferenceNode(ref XElement refrenceNode, string dllFilePath)
        {
            refrenceNode.SetElementValue(XName.Get("SpecificVersion", refrenceNode.Name.NamespaceName), "False");
            refrenceNode.SetElementValue(XName.Get("HintPath", refrenceNode.Name.NamespaceName), dllFilePath);
            return refrenceNode;
        }

        public static XElement CreateReferenceNode(string name, string dllFilePath, string namespaceName)
        {
            XElement xElement = new XElement(XName.Get("Reference", namespaceName));
            xElement.SetAttributeValue("Include", name);
            xElement.SetElementValue(XName.Get("HintPath", namespaceName), dllFilePath);
            return xElement;
        }

        public static XElement CreateXElement(string text)
        {
            return XElement.Parse(text);
        }


    }
}
