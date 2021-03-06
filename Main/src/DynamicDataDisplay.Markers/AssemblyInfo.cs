﻿using System.Windows.Markup;
using Microsoft.Research.DynamicDataDisplay;
using System.Security;
using System.Runtime.CompilerServices;

[assembly: XmlnsDefinition(D3AssemblyConstants.DefaultXmlNamespace, "Microsoft.Research.DynamicDataDisplay.Charts")]
[assembly: XmlnsDefinition(D3AssemblyConstants.DefaultXmlNamespace, "DynamicDataDisplay.Markers")]
//[assembly: XmlnsDefinition(D3AssemblyConstants.DefaultXmlNamespace, "Microsoft.Research.DynamicDataDisplay.Editors")]
[assembly: XmlnsDefinition(D3AssemblyConstants.DefaultXmlNamespace, "DynamicDataDisplay.Markers.MarkerGenerators")]
[assembly: XmlnsDefinition(D3AssemblyConstants.DefaultXmlNamespace, "Microsoft.Research.DynamicDataDisplay.Charts.NewLine")]
[assembly: XmlnsDefinition(D3AssemblyConstants.DefaultXmlNamespace, "Microsoft.Research.DynamicDataDisplay.Charts.Selectors")]
[assembly: XmlnsDefinition(D3AssemblyConstants.DefaultXmlNamespace, "Microsoft.Research.DynamicDataDisplay.Markers.MarkerGenerators.Rendering")]

[assembly: AllowPartiallyTrustedCallers]

//[assembly: XmlnsDefinition(D3AssemblyConstants.DefaultXmlNamespace, "DynamicDataDisplay.Markers.Common")]
//[assembly: XmlnsDefinition(D3AssemblyConstants.DefaultXmlNamespace, "DynamicDataDisplay.Markers.Panels")]
