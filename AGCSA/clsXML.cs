using System;
using System.IO;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing;

namespace AGCSA
{
	internal partial class clsXML
	{
		private ActiveGanttCSACtl mp_oControl;
		private System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
		private System.Xml.XmlElement oControlElement;
		private System.Xml.XmlElement oFontElement;
        private System.Xml.XmlElement oDateTimeElement;
		private String mp_sObject;
		private PE_LEVEL mp_yLevel;
        private bool mp_bSupportOptional = false;
        private bool mp_bBoolsAreNumeric = false;

        private enum PE_LEVEL
        {
            LVL_CONTROL = 0,
            LVL_FONT = 5,
            LVL_DATETIME = 6,
        }

        internal bool SupportOptional
        {
            get { return mp_bSupportOptional; }
            set { mp_bSupportOptional = value; }
        }

        internal bool BoolsAreNumeric
        {
            get { return mp_bBoolsAreNumeric; }
            set { mp_bBoolsAreNumeric = value; }
        }

		internal clsXML(ActiveGanttCSACtl Value, String sObject)
		{
			mp_oControl = Value;
			xDoc = new System.Xml.XmlDocument();
			xDoc.PreserveWhitespace = false;
			mp_sObject = sObject;
		}

		~clsXML()
		{
		}

		internal void InitializeWriter()
		{
			xDoc.LoadXml("<" + mp_sObject + "></" + mp_sObject + ">");
			oControlElement = GetDocumentElement(mp_sObject, 0);
			mp_yLevel = PE_LEVEL.LVL_CONTROL;
		}

		internal void InitializeReader()
		{
			oControlElement = GetDocumentElement(mp_sObject, 0);
			mp_yLevel = PE_LEVEL.LVL_CONTROL;
		}

        internal System.Xml.XmlDocument GetDocument
        {
            get { return xDoc; }
        }

        internal void AddAttribute(string sName, string sValue)
        {
            XmlAttribute oAttribute = xDoc.CreateAttribute(sName);
            oAttribute.Value = sValue;
            xDoc.DocumentElement.Attributes.Append(oAttribute);
        }

		internal void WriteXML(String sPath)
		{
			XmlTextWriter oWriter = new XmlTextWriter(sPath, System.Text.Encoding.UTF8);
			oWriter.IndentChar = '\t';
			oWriter.Formatting = Formatting.Indented;
			oWriter.WriteStartDocument();
			
			xDoc.Save(oWriter);
			oWriter.WriteEndDocument();
			oWriter.Close();
		}

		internal void ReadXML(String sPath)
		{
			xDoc.Load(sPath);
		}

		private System.Xml.XmlElement ParentElement()
		{
            switch (mp_yLevel)
            {
                case PE_LEVEL.LVL_CONTROL:
                    return oControlElement;
                case PE_LEVEL.LVL_FONT:
                    return oFontElement;
                case PE_LEVEL.LVL_DATETIME:
                    return oDateTimeElement;
            }
            return null;
		}

		private System.Xml.XmlElement mp_oCreateEmptyDOMElement(String sElementName)
		{
			System.Xml.XmlElement oNodeBuff;
			oNodeBuff = xDoc.CreateElement(sElementName);
			ParentElement().AppendChild(oNodeBuff);
			return oNodeBuff;
		}

		private System.Xml.XmlElement GetDocumentElement(String TagName, int lIndex)
		{
			return (System.Xml.XmlElement) xDoc.GetElementsByTagName(TagName).Item(lIndex);
		}

		internal String GetXML()
		{
			return xDoc.InnerXml;
		}

		internal void SetXML(String sXML)
		{
            if (mp_bSupportOptional == false)
            {
                xDoc.LoadXml(sXML);
            }
            else
            {
                if (sXML.Length > 0)
                {
                    xDoc.LoadXml(sXML);
                }
            }
		}

        internal String ReadObject(String sObjectName)
        {
            if (mp_bSupportOptional == false)
            {
                return ParentElement().GetElementsByTagName(sObjectName).Item(0).OuterXml;
            }
            else
            {
                if (ParentElement() == null)
                {
                    return "";
                }
                if (ParentElement().GetElementsByTagName(sObjectName).Count > 0)
                {
                    return ParentElement().GetElementsByTagName(sObjectName).Item(0).OuterXml;
                }
                else
                {
                    return "";
                }
            }
        }

        internal String ReadCollectionObject(int lIndex)
        {
            if (mp_bSupportOptional == false)
            {
                return ParentElement().ChildNodes.Item(lIndex - 1).OuterXml;
            }
            else
            {
                if (ParentElement() == null | lIndex == 0)
                {
                    return "";
                }
                if (ParentElement().ChildNodes.Count > 0)
                {
                    return ParentElement().ChildNodes.Item(lIndex - 1).OuterXml;
                }
                else
                {
                    return "";
                }
            }
        }

        internal string GetCollectionObjectName(int lIndex)
        {
            return ParentElement().ChildNodes.Item(lIndex - 1).Name;
        }

		internal int ReadCollectionCount()
		{
            if (mp_bSupportOptional == false)
            {
                return ParentElement().ChildNodes.Count;
            }
            else
            {
                if (ParentElement() == null)
                {
                    return 0;
                }
                else
                {
                    return ParentElement().ChildNodes.Count;
                }
            }
		}

		internal void ReadProperty(String sElementName, ref int sElementValue)
		{
			sElementValue = lReadProperty(sElementName, sElementValue);
		}

		internal void ReadProperty(String sElementName, ref short sElementValue)
		{
			sElementValue = iReadProperty(sElementName, sElementValue);
		}

		internal void ReadProperty(String sElementName, ref string sElementValue)
		{
			sElementValue = sReadProperty(sElementName, sElementValue);
		}

		internal void ReadProperty(String sElementName, ref bool sElementValue)
		{
            sElementValue = bReadProperty(sElementName, sElementValue);
		}

		internal void ReadProperty(String sElementName, ref System.DateTime sElementValue)
		{
			sElementValue = dtReadProperty(sElementName, sElementValue);
		}

        private int lReadProperty(string v_sNodeName, int sElementValue)
        {
            if (mp_bSupportOptional == false)
            {
                return System.Convert.ToInt32(ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText);
            }
            else
            {
                if (ParentElement() == null)
                {
                    return sElementValue;
                }
                if (ParentElement().GetElementsByTagName(v_sNodeName).Count > 0)
                {
                    return System.Convert.ToInt32(ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText);
                }
                else
                {
                    return sElementValue;
                }
            }
        }

        private short iReadProperty(string v_sNodeName, short sElementValue)
        {
            if (mp_bSupportOptional == false)
            {
                return System.Convert.ToInt16(ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText);
            }
            else
            {
                if (ParentElement() == null)
                {
                    return sElementValue;
                }
                if (ParentElement().GetElementsByTagName(v_sNodeName).Count > 0)
                {
                    return System.Convert.ToInt16(ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText);
                }
                else
                {
                    return sElementValue;
                }
            }
        }

        private string sReadProperty(string v_sNodeName, string sElementValue)
        {
            if (mp_bSupportOptional == false)
            {
                return ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText;
            }
            else
            {
                if (ParentElement() == null)
                {
                    return sElementValue;
                }
                if (ParentElement().GetElementsByTagName(v_sNodeName).Count > 0)
                {
                    if (ParentElement().GetElementsByTagName(v_sNodeName).Item(0).ParentNode.Name == ParentElement().Name)
                    {
                        string sReturn = null;
                        sReturn = ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText;
                        return sReturn;
                    }
                    else
                    {
                        return sElementValue;
                    }
                }
                else
                {
                    return sElementValue;
                }
            }
        }

        private bool bReadProperty(string v_sNodeName, bool bElementValue)
        {
            if (mp_bSupportOptional == false)
            {
                if (ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText == "false" | ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (ParentElement() == null)
                {
                    return bElementValue;
                }
                if (ParentElement().GetElementsByTagName(v_sNodeName).Count > 0)
                {
                    if (ParentElement().GetElementsByTagName(v_sNodeName).Item(0).ParentNode.Name == ParentElement().Name)
                    {
                        if (ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText == "false" | ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText == "0")
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return bElementValue;
                    }
                }
                else
                {
                    return bElementValue;
                }
            }
        }

        private System.DateTime dtReadProperty(string v_sNodeName, System.DateTime dtElementValue)
        {
            if (mp_bSupportOptional == false)
            {
                return mp_dtGetDateFromXML(ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText);
            }
            else
            {
                if (ParentElement() == null)
                {
                    return dtElementValue;
                }
                if (ParentElement().GetElementsByTagName(v_sNodeName).Count > 0)
                {
                    return mp_dtGetDateFromXML(ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText);
                }
                else
                {
                    return dtElementValue;
                }
            }
        }

        private float fReadProperty(string v_sNodeName, float fElementValue)
        {
            if (mp_bSupportOptional == false)
            {
                return System.Convert.ToSingle(ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText);
            }
            else
            {
                if (ParentElement() == null)
                {
                    return fElementValue;
                }
                if (ParentElement().GetElementsByTagName(v_sNodeName).Count > 0)
                {
                    return System.Convert.ToSingle(ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText);
                }
                else
                {
                    return fElementValue;
                }
            }
        }

        private int yReadProperty(string v_sNodeName, int yElementValue)
        {
            if (mp_bSupportOptional == false)
            {
                return System.Convert.ToInt16(ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText);
            }
            else
            {
                if (ParentElement() == null)
                {
                    return yElementValue;
                }
                if (ParentElement().GetElementsByTagName(v_sNodeName).Count > 0)
                {
                    return System.Convert.ToInt16(ParentElement().GetElementsByTagName(v_sNodeName).Item(0).InnerText);
                }
                else
                {
                    return yElementValue;
                }
            }

        }

        private System.DateTime mp_dtGetDateFromXML(String sParam)
        {
            System.DateTime dtReturn;
            int lYear = System.Convert.ToInt32(sParam.Substring(0, 4));
            int lMonth = System.Convert.ToInt32(sParam.Substring(5, 2));
            int lDay = System.Convert.ToInt32(sParam.Substring(8, 2));
            int lHours = System.Convert.ToInt32(sParam.Substring(11, 2));
            int lMinutes = System.Convert.ToInt32(sParam.Substring(14, 2));
            int lSeconds = System.Convert.ToInt32(sParam.Substring(17, 2));
            dtReturn = new System.DateTime(lYear, lMonth, lDay, lHours, lMinutes, lSeconds);
            return dtReturn;
        }

		internal void ReadProperty(String sElementName, ref float sElementValue)
		{
			sElementValue = fReadProperty(sElementName, sElementValue);
		}

		internal void ReadProperty(String sElementName, ref Color sElementValue)
		{
			sElementValue = ColorTranslator.FromOle(mp_oControl.StrLib.StrCLng(ParentElement().GetElementsByTagName(sElementName).Item(0).InnerText));
		}

		internal void ReadProperty(String sElementName, ref Font r_oFont)
		{
			FontStyle iStyle = new FontStyle();
			PE_LEVEL mp_yBackupLevel;
			String sName = "";
			float fSize = 0;
			bool bDummy = false;
			oFontElement = (System.Xml.XmlElement) ParentElement().GetElementsByTagName(sElementName).Item(0);
			mp_yBackupLevel = mp_yLevel;
			mp_yLevel = PE_LEVEL.LVL_FONT;
			ReadProperty("Bold", ref bDummy);
			if (bDummy == true )
			{
				iStyle = iStyle | FontStyle.Bold;
			}
			ReadProperty("Italic", ref bDummy);
			if (bDummy == true )
			{
				iStyle = iStyle | FontStyle.Italic;
			}
			ReadProperty("Underline", ref bDummy);
			if (bDummy == true )
			{
				iStyle = iStyle | FontStyle.Underline;
			}
			ReadProperty("Name", ref sName);
			ReadProperty("Size", ref fSize);
			if (sName == "MS Sans Serif")
			{
				sName = "Microsoft Sans Serif";
			}
			Font oFont = new Font(sName, fSize, iStyle);
			mp_yLevel = mp_yBackupLevel;
			r_oFont = oFont;
		}

        internal void ReadProperty(String sElementName, ref AGCSA.DateTime oDate)
        {
            PE_LEVEL mp_yBackupLevel;
            oDateTimeElement = (System.Xml.XmlElement)ParentElement().GetElementsByTagName(sElementName).Item(0);
            mp_yBackupLevel = mp_yLevel;
            mp_yLevel = PE_LEVEL.LVL_DATETIME;
            System.DateTime dtDateTime = new System.DateTime(0);
            int lSecondFraction = 0;
            ReadProperty("DateTime", ref dtDateTime);
            ReadProperty("SecondFraction", ref lSecondFraction);
            oDate.DateTimePart = dtDateTime;
            oDate.SecondFractionPart = lSecondFraction;
            mp_yLevel = mp_yBackupLevel;
        }

		internal void WriteObject(String sObjectText)
		{
			System.Xml.XmlDocument xDoc1;
			System.Xml.XmlElement oNodeBuff;
			xDoc1 = new System.Xml.XmlDocument();
			xDoc1.LoadXml(sObjectText);
			oNodeBuff = (System.Xml.XmlElement) xDoc.ImportNode(xDoc1.DocumentElement, true);
			ParentElement().AppendChild(oNodeBuff);
		}

		internal void WriteProperty(String sElementName, Object sElementValue)
		{
			System.Xml.XmlElement oNodeBuff;
			oNodeBuff = xDoc.CreateElement(sElementName);
			if (sElementValue == null)
			{
				System.Diagnostics.Debug.WriteLine("");
			}
			if (sElementValue.GetType().FullName == "System.DateTime")
			{
				oNodeBuff.InnerText = mp_sGetXMLDateString(System.Convert.ToDateTime(sElementValue));
			}
			else if (sElementValue.GetType().FullName == "System.Boolean")
			{
				if (System.Convert.ToBoolean(sElementValue) == true)
				{
                    if (mp_bBoolsAreNumeric == true)
                    {
                        oNodeBuff.InnerText = "1";
                    }
                    else
                    {
                        oNodeBuff.InnerText = "true";
                    }
				}
				else
				{
                    if (mp_bBoolsAreNumeric == true)
                    {
                        oNodeBuff.InnerText = "0";
                    }
                    else
                    {
                        oNodeBuff.InnerText = "false";
                    }
					
				}
			}
			else if (sElementValue.GetType().FullName == "System.Drawing.Color")
			{
				Color oColor;
				oColor = (Color) sElementValue;
				oNodeBuff.InnerText = mp_oControl.StrLib.StrCStr(ColorTranslator.ToOle(oColor));
			}
			else if (sElementValue.GetType().IsEnum == true)
			{
				oNodeBuff.InnerText = System.Convert.ToInt32(sElementValue).ToString();
			}
			else
			{
				oNodeBuff.InnerText = sElementValue.ToString();
			}
			ParentElement().AppendChild(oNodeBuff);
		}

		private String mp_sGetXMLDateString(System.DateTime dtParam)
		{
            return mp_oControl.StrLib.StrFormat(dtParam.Year, "0000") + "-" + mp_oControl.StrLib.StrFormat(dtParam.Month, "00") + "-" + mp_oControl.StrLib.StrFormat(dtParam.Day, "00") + "T" + mp_oControl.StrLib.StrFormat(dtParam.Hour, "00") + ":" + mp_oControl.StrLib.StrFormat(dtParam.Minute, "00") + ":" + mp_oControl.StrLib.StrFormat(dtParam.Second, "00");
		}

		internal void WriteProperty(String sElementName, Font oFont)
		{
			PE_LEVEL mp_yBackupLevel;
			oFontElement = mp_oCreateEmptyDOMElement(sElementName);
			mp_yBackupLevel = mp_yLevel;
			mp_yLevel = PE_LEVEL.LVL_FONT;
			WriteProperty("Name", oFont.Name);
			WriteProperty("Size", mp_oControl.StrLib.StrReplace(mp_oControl.StrLib.StrCStr(oFont.Size), mp_oControl.StrLib.GetDecimalSeparator(), "."));
			WriteProperty("Bold", oFont.Bold);
			WriteProperty("Italic", oFont.Italic);
			WriteProperty("Underline", oFont.Underline);
			mp_yLevel = mp_yBackupLevel;
		}

        internal void WriteProperty(String sElementName, AGCSA.DateTime oDate)
        {
            PE_LEVEL mp_yBackupLevel;
            mp_yBackupLevel = mp_yLevel;
            oDateTimeElement = mp_oCreateEmptyDOMElement(sElementName);
            mp_yLevel = PE_LEVEL.LVL_DATETIME;
            WriteProperty("DateTime", oDate.DateTimePart);
            WriteProperty("SecondFraction", oDate.SecondFractionPart);
            mp_yLevel = mp_yBackupLevel;
        }

		internal void WriteProperty(String sElementName, ref Image oPicture)
		{
			String sObjectText;
			System.Xml.XmlElement oNodeBuff;
			if (oPicture != null)
			{

				System.Xml.XmlDocument xDoc1;
				xDoc1 = new System.Xml.XmlDocument();
				sObjectText = "<" + sElementName + " xmlns:dt=\"urn:schemas-microsoft-com:datatypes\" dt:dt=\"bin.base64\"></" + sElementName + ">";
				xDoc1.LoadXml(sObjectText);
				oNodeBuff = (System.Xml.XmlElement) xDoc.ImportNode(xDoc1.DocumentElement, true);
				System.IO.MemoryStream mem = new System.IO.MemoryStream();
				String data;
				oPicture.Save(mem, ImageFormat.Png);
				data = Convert.ToBase64String(mem.ToArray());
				oNodeBuff.InnerText = data;
			}
			else
			{
				oNodeBuff = xDoc.CreateElement(sElementName);
			}
			ParentElement().AppendChild(oNodeBuff);
		}

		internal void ReadProperty(String sElementName, ref Image oPicture)
		{
			if (ParentElement().GetElementsByTagName(sElementName).Item(0).InnerText != "")
			{
				String data = ParentElement().GetElementsByTagName(sElementName).Item(0).InnerText;
				System.IO.MemoryStream mem = new System.IO.MemoryStream(Convert.FromBase64String(data));
				Bitmap bmp = new Bitmap(mem, false);
				oPicture = bmp;
			}
			else
			{
				oPicture = null;
			}
		}

		internal void ReadProperty(String sElementName, ref E_ADDMODE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_ADDMODE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_BORDERSTYLE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_BORDERSTYLE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_TEXTPLACEMENT sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_TEXTPLACEMENT) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_LAYEROBJECTENABLE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_LAYEROBJECTENABLE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_MOVEMENTTYPE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_MOVEMENTTYPE) yBuff;
		}


		internal void ReadProperty(String sElementName, ref E_OBJECTTYPE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_OBJECTTYPE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_PLACEMENT sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_PLACEMENT) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_PROGRESSLINELENGTH sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_PROGRESSLINELENGTH) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_PROGRESSLINETYPE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_PROGRESSLINETYPE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_REPORTERRORS sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_REPORTERRORS) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_SCROLLBEHAVIOUR sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_SCROLLBEHAVIOUR) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_STYLEAPPEARANCE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_STYLEAPPEARANCE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_TICKMARKTYPES sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_TICKMARKTYPES) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_TIERPOSITION sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_TIERPOSITION) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_TIERTYPE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_TIERTYPE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_TIMEBLOCKBEHAVIOUR sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_TIMEBLOCKBEHAVIOUR) yBuff;
		}

		internal void ReadProperty(String sElementName, ref GRE_HATCHSTYLE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (GRE_HATCHSTYLE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref GRE_BUTTONSTYLE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (GRE_BUTTONSTYLE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref GRE_BACKGROUNDMODE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (GRE_BACKGROUNDMODE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref GRE_FIGURETYPE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (GRE_FIGURETYPE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref GRE_GRADIENTFILLMODE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (GRE_GRADIENTFILLMODE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref GRE_HORIZONTALALIGNMENT sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (GRE_HORIZONTALALIGNMENT) yBuff;
		}

		internal void ReadProperty(String sElementName, ref GRE_LINEDRAWSTYLE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (GRE_LINEDRAWSTYLE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref GRE_VERTICALALIGNMENT sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (GRE_VERTICALALIGNMENT) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_CONSTRAINTTYPE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_CONSTRAINTTYPE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_RECURRINGTYPE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_RECURRINGTYPE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_TIMEBLOCKTYPE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_TIMEBLOCKTYPE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_WEEKDAY sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_WEEKDAY) yBuff;
		}

		internal void ReadProperty(String sElementName, ref GRE_BORDERSTYLE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (GRE_BORDERSTYLE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref GRE_FILLMODE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (GRE_FILLMODE) yBuff;
		}

		internal void ReadProperty(String sElementName, ref GRE_PATTERN sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (GRE_PATTERN) yBuff;
		}

		internal void ReadProperty(String sElementName, ref E_CONTROLMODE sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (E_CONTROLMODE) yBuff;
		}

        internal void ReadProperty(String sElementName, ref E_INTERVAL sElementValue)
        {
            short yBuff = 0;
            ReadProperty(sElementName, ref yBuff);
            sElementValue = (E_INTERVAL)yBuff;
        }

        internal void ReadProperty(String sElementName, ref E_SPLITTERTYPE sElementValue)
        {
            short yBuff = 0;
            ReadProperty(sElementName, ref yBuff);
            sElementValue = (E_SPLITTERTYPE)yBuff;
        }

        internal void ReadProperty(String sElementName, ref E_TIERBACKGROUNDMODE sElementValue)
        {
            short yBuff = 0;
            ReadProperty(sElementName, ref yBuff);
            sElementValue = (E_TIERBACKGROUNDMODE)yBuff;
        }

        internal void ReadProperty(String sElementName, ref E_TIERAPPEARANCESCOPE sElementValue)
        {
            short yBuff = 0;
            ReadProperty(sElementName, ref yBuff);
            sElementValue = (E_TIERAPPEARANCESCOPE)yBuff;
        }

        internal void ReadProperty(String sElementName, ref E_TIERFORMATSCOPE sElementValue)
        {
            short yBuff = 0;
            ReadProperty(sElementName, ref yBuff);
            sElementValue = (E_TIERFORMATSCOPE)yBuff;
        }

        internal void ReadProperty(String sElementName, ref E_SELECTIONRECTANGLEMODE sElementValue)
        {
            short yBuff = 0;
            ReadProperty(sElementName, ref yBuff);
            sElementValue = (E_SELECTIONRECTANGLEMODE)yBuff;
        }

        internal void ReadProperty(String sElementName, ref E_PREDECESSORMODE sElementValue)
        {
            short yBuff = 0;
            ReadProperty(sElementName, ref yBuff);
            sElementValue = (E_PREDECESSORMODE)yBuff;
        }

        internal void ReadProperty(String sElementName, ref E_TASKTYPE sElementValue)
        {
            short yBuff = 0;
            ReadProperty(sElementName, ref yBuff);
            sElementValue = (E_TASKTYPE)yBuff;
        }

        internal void ReadProperty(String sElementName, ref E_TBINTERVALTYPE sElementValue)
        {
            short yBuff = 0;
            ReadProperty(sElementName, ref yBuff);
            sElementValue = (E_TBINTERVALTYPE)yBuff;
        }

		internal void ReadProperty(String sElementName, ref byte sElementValue)
		{
			short yBuff = 0;
			ReadProperty(sElementName, ref yBuff);
			sElementValue = (byte) yBuff;
		}
	}
}
