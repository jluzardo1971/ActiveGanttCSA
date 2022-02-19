using System;
using System.Collections;

namespace AGCSA
{
	public class clsTasks
	{

		private ActiveGanttCSACtl mp_oControl;
		private clsCollectionBase mp_oCollection;

        int mp_lLoadIndex = 0;
        private ArrayList mp_oTempCollection = null;
        private clsDictionary mp_oTempDictionary = null;

		internal clsTasks(ActiveGanttCSACtl Value)
		{
			mp_oControl = Value;
			mp_oCollection = new clsCollectionBase(Value, "Task");
		}

		public int Count 
		{
			get { return mp_oCollection.m_lCount(); }
		}

		public clsTask Item(string Index)
		{
			return (clsTask) mp_oCollection.m_oItem(Index, SYS_ERRORS.TASKS_ITEM_1, SYS_ERRORS.TASKS_ITEM_2, SYS_ERRORS.TASKS_ITEM_3, SYS_ERRORS.TASKS_ITEM_4);
		}

		internal clsCollectionBase oCollection 
		{
			get { return mp_oCollection; }
		}

		public clsTask Add(string Text, string RowKey, AGCSA.DateTime StartDate, AGCSA.DateTime EndDate, string Key, string StyleIndex,	string LayerIndex)
		{
			mp_oCollection.AddMode = true;
			clsTask oTask = new clsTask(mp_oControl);
			Key = mp_oControl.StrLib.StrTrim(Key);
			Text = mp_oControl.StrLib.StrTrim(Text);
			RowKey = mp_oControl.StrLib.StrTrim(RowKey);
			oTask.Text = Text;
			oTask.RowKey = RowKey;
			oTask.StartDate = StartDate;
			oTask.EndDate = EndDate;
			oTask.Key = Key;
			oTask.StyleIndex = StyleIndex;
			oTask.LayerIndex = LayerIndex;
			mp_oCollection.m_Add(oTask, Key, SYS_ERRORS.TASKS_ADD_1, SYS_ERRORS.TASKS_ADD_2, false, SYS_ERRORS.TASKS_ADD_3);
			return oTask;
		}

        public clsTask DAdd(string RowKey, AGCSA.DateTime StartDate, E_INTERVAL DurationInterval, int DurationFactor)
        {
            return DAdd(RowKey, StartDate, DurationInterval, DurationFactor, "", "", "", "0");
        }

        public clsTask DAdd(string RowKey, AGCSA.DateTime StartDate, E_INTERVAL DurationInterval, int DurationFactor, string Text)
        {
            return DAdd(RowKey, StartDate, DurationInterval, DurationFactor, Text, "", "", "0");
        }

        public clsTask DAdd(string RowKey, AGCSA.DateTime StartDate, E_INTERVAL DurationInterval, int DurationFactor, string Text, string Key)
        {
            return DAdd(RowKey, StartDate, DurationInterval, DurationFactor, Text, Key, "", "0");
        }

        public clsTask DAdd(string RowKey, AGCSA.DateTime StartDate, E_INTERVAL DurationInterval, int DurationFactor, string Text, string Key, string StyleIndex)
        {
            return DAdd(RowKey, StartDate, DurationInterval, DurationFactor, Text, Key, StyleIndex, "0");
        }

        public clsTask DAdd(string RowKey, AGCSA.DateTime StartDate, E_INTERVAL DurationInterval, int DurationFactor, string Text, string Key, string StyleIndex, string LayerIndex)
	    {
		    mp_oCollection.AddMode = true;
		    clsTask oTask = new clsTask(mp_oControl);
		    oTask.TaskType = E_TASKTYPE.TT_DURATION;
		    Key = mp_oControl.StrLib.StrTrim(Key);
		    Text = mp_oControl.StrLib.StrTrim(Text);
		    RowKey = mp_oControl.StrLib.StrTrim(RowKey);
		    oTask.Text = Text;
		    oTask.RowKey = RowKey;
		    oTask.StartDate = StartDate;
		    oTask.DurationInterval = DurationInterval;
		    oTask.DurationFactor = DurationFactor;
		    oTask.Key = Key;
		    oTask.StyleIndex = StyleIndex;
		    oTask.LayerIndex = LayerIndex;
		    mp_oCollection.m_Add(oTask, Key, SYS_ERRORS.TASKS_ADD_1, SYS_ERRORS.TASKS_ADD_2, false, SYS_ERRORS.TASKS_ADD_3);
		    return oTask;
	    }

		public void Clear()
		{
            mp_oControl.Predecessors.Clear();
			mp_oControl.Percentages.Clear();
			mp_oCollection.m_Clear();
			mp_oControl.SelectedTaskIndex = 0;
		}

		public void Remove(string Index)
		{
			int lIndex = 0;
			string sRIndex = "";
			string sRKey = "";
            clsPredecessor oPredecessor = null;
			mp_oCollection.m_GetKeyAndIndex(Index, ref sRKey, ref sRIndex);
			mp_oControl.Percentages.oCollection.m_CollectionRemoveWhere("TaskKey", sRKey, sRIndex);
            if (sRKey.Length > 0)
            {
                for (lIndex = mp_oControl.Predecessors.Count; lIndex >= 1; lIndex--)
                {
                    oPredecessor = (clsPredecessor)mp_oControl.Predecessors.oCollection.m_oReturnArrayElement(lIndex);
                    if (oPredecessor.SuccessorKey == sRKey | oPredecessor.PredecessorKey == sRKey)
                    {
                        mp_oControl.Predecessors.Remove(lIndex.ToString());
                    }
                }
            }
			mp_oCollection.m_Remove(Index, SYS_ERRORS.TASKS_REMOVE_1, SYS_ERRORS.TASKS_REMOVE_2, SYS_ERRORS.TASKS_REMOVE_3, SYS_ERRORS.TASKS_REMOVE_4);
			mp_oControl.SelectedTaskIndex = 0;
		}

		public void Sort(string PropertyName, bool Descending, E_SORTTYPE SortType, int StartIndex, int EndIndex)
		{
			if (StartIndex == -1) 
			{
				StartIndex = 1;
			}
			if (EndIndex == -1) 
			{
				EndIndex = Count;
			}
			if (Count == 0) return; 
			if (StartIndex < 1 | StartIndex > Count) 
			{
				return;
			}
			if (EndIndex < 1 | EndIndex > Count) 
			{
				return;
			}
			if (EndIndex == StartIndex) 
			{
				return;
			}
			mp_oCollection.m_Sort(PropertyName, Descending, SortType, StartIndex, EndIndex);
		}

		internal void Draw()
		{
			int lIndex = 0;
			clsTask oTask = null;
			if (Count == 0) 
			{
				return;
			}
			for (lIndex = 1; lIndex <= Count; lIndex++) 
			{
				oTask = (clsTask) mp_oCollection.m_oReturnArrayElement(lIndex);
                if (oTask.Visible == true & oTask.InsideVisibleTimeLineArea == true) 
				{
					mp_oControl.DrawEventArgs.Clear();
					mp_oControl.DrawEventArgs.CustomDraw = false;
					mp_oControl.DrawEventArgs.EventTarget = E_EVENTTARGET.EVT_TASK;
					mp_oControl.DrawEventArgs.ObjectIndex = lIndex;
					mp_oControl.DrawEventArgs.ParentObjectIndex = 0;
					mp_oControl.DrawEventArgs.Graphics = mp_oControl.clsG.oGraphics;
					mp_oControl.FireDraw();
					if (mp_oControl.DrawEventArgs.CustomDraw == false) 
					{
						if (oTask.Type == E_OBJECTTYPE.OT_MILESTONE) 
						{
                            mp_oControl.clsG.mp_DrawItemI(oTask, "", lIndex == mp_oControl.SelectedTaskIndex, mp_GetTaskStyle(oTask));
						}
						else 
						{
                            mp_oControl.clsG.mp_DrawItem(oTask.Left, oTask.Top, oTask.Right, oTask.Bottom, "", oTask.Text, (lIndex == mp_oControl.SelectedTaskIndex), oTask.Image, oTask.LeftTrim, oTask.RightTrim, mp_GetTaskStyle(oTask));
						}
					}
				}
			}
		}

        private clsStyle mp_GetTaskStyle(clsTask oTask)
        {
            clsStyle oStyle;
            if (oTask.mp_bWarning == true)
            {
                oStyle = oTask.WarningStyle;
            }
            else
            {
                oStyle = oTask.Style;
            }
            return oStyle;
        }

		public string GetXML()
		{
			int lIndex = 0;
			clsTask oTask = null;
			clsXML oXML = new clsXML(mp_oControl, "Tasks");
			oXML.InitializeWriter();
			for (lIndex = 1; lIndex <= Count; lIndex++) 
			{
				oTask = (clsTask) mp_oCollection.m_oReturnArrayElement(lIndex);
				oXML.WriteObject(oTask.GetXML());
			}
			return oXML.GetXML();
		}

		public void SetXML(string sXML)
		{
			int lIndex = 0;
			clsXML oXML = new clsXML(mp_oControl, "Tasks");
			oXML.SetXML(sXML);
			oXML.InitializeReader();
			mp_oCollection.m_Clear();
			for (lIndex = 1; lIndex <= oXML.ReadCollectionCount(); lIndex++) 
			{
				clsTask oTask = new clsTask(mp_oControl);
				oTask.SetXML(oXML.ReadCollectionObject(lIndex));
				mp_oCollection.AddMode = true;
				mp_oCollection.m_Add(oTask, oTask.Key, SYS_ERRORS.TASKS_ADD_1, SYS_ERRORS.TASKS_ADD_2, false, SYS_ERRORS.TASKS_ADD_3);
				oTask = null;
			}
		}

        public void BeginLoad(bool Preserve)
        {
            if (Preserve == false)
            {
                mp_lLoadIndex = 1;
                mp_oTempCollection = new ArrayList();
                mp_oTempDictionary = new clsDictionary();
                mp_oCollection.mp_aoCollection.Clear();
                mp_oCollection.mp_oKeys.Clear();
            }
            else
            {
                mp_oTempCollection = mp_oCollection.mp_aoCollection;
                mp_oCollection.mp_aoCollection = null;
                mp_oCollection.mp_aoCollection = new ArrayList();
                mp_oTempDictionary = mp_oCollection.mp_oKeys;
                mp_oCollection.mp_oKeys = null;
                mp_oCollection.mp_oKeys = new clsDictionary();
                mp_lLoadIndex = mp_oTempCollection.Count + 1;
            }
        }

        public clsTask Load(string sRowKey, string sKey)
        {
            clsTask oTask = new clsTask(mp_oControl);
            if (sKey.Length > 0)
            {
                oTask.Key = sKey;
            }
            oTask.Index = mp_lLoadIndex;
            if (mp_oControl.Rows.mp_oTempCollection.Count > 0)
            {
                Object lRowIndex;
                lRowIndex = mp_oControl.Rows.mp_oTempDictionary[sRowKey];
                if (lRowIndex != null)
                {
                    oTask.mp_oRow = (clsRow)mp_oControl.Rows.mp_oTempCollection[System.Convert.ToInt32(lRowIndex) - 1];
                }
                else
                {
                    lRowIndex = mp_oControl.Rows.oCollection.mp_oKeys[sRowKey];
                    oTask.mp_oRow = (clsRow)mp_oControl.Rows.oCollection.mp_aoCollection[System.Convert.ToInt32(lRowIndex) - 1];
                }
            }
            else
            {
                int lRowIndex;
                lRowIndex = mp_oControl.Rows.oCollection.mp_oKeys[sRowKey];
                oTask.mp_oRow = (clsRow)mp_oControl.Rows.oCollection.mp_aoCollection[lRowIndex - 1];
            }
            mp_oTempCollection.Add(oTask);
            mp_oTempDictionary.Add(mp_lLoadIndex, sKey);
            mp_lLoadIndex = mp_lLoadIndex + 1;
            return oTask;
        }

        public void EndLoad()
        {
            mp_oCollection.mp_aoCollection = mp_oTempCollection;
            mp_oCollection.mp_oKeys = mp_oTempDictionary;
            mp_oTempCollection = null;
            mp_oTempDictionary = null;
        }

	}

}

