using System;
using System.ComponentModel;
using System.Reflection;

namespace AGCSA
{

    internal struct S_Rectangle
    {
        internal int X1;
        internal int Y1;
        internal int X2;

        internal int Y2;
        internal bool mp_bInRect(int X, int Y)
        {
            if (X >= X1 & X <= X2 & Y >= Y1 & Y <= Y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    internal struct S_Point
    {
        internal int X;
        internal int Y;
    }

    internal struct S_TIMEBLOCK
    {
        public AGCSA.DateTime dtStart;
        public AGCSA.DateTime dtEnd;
    }

	internal enum E_CURSORTYPE
	{
		CT_NORMAL = 0,
		CT_SIZETASK = 1,
		CT_MOVETASK = 2,
		CT_MOVEMILESTONE = 3,
		CT_CLIENTAREA = 4,
		CT_MOVESPLITTER = 5,
		CT_ROWHEIGHT = 6,
		CT_COLUMNWIDTH = 7,
		CT_MOVEROW = 8,
		CT_MOVECOLUMN = 11,
		CT_SCROLLTIMELINE = 9,
		CT_NODROP = 10,
		CT_PERCENTAGE = 12,
		CT_PREDECESSOR = 13
	}

	internal enum E_MOUSEKEYBOARDEVENTS
	{
		MouseHover = 0,
		MouseDown = 1,
		MouseMove = 2,
		MouseUp = 3,
		MouseClick = 4,
		MouseDblClick = 5,
		MouseWheel = 6,
		KeyPress = 7,
		KeyDown = 8,
		KeyUp = 9
	}

	internal enum E_CLIENTAREAVISIBILITY
	{
		VS_ABOVEVISIBLEAREA = 0,
		VS_INSIDEVISIBLEAREA = 1,
		VS_BELOWVISIBLEAREA = 2,
        VS_LEFTOFVISIBLEAREA = 3,
        VS_RIGHTOFVISIBLEAREA = 4,
	}

	public enum GRE_ARROWDIRECTION
	{
		AWD_UP = 0,
		AWD_DOWN = 1,
		AWD_LEFT = 2,
		AWD_RIGHT = 3
	}

	public enum GRE_LINEDRAWSTYLE
	{
		LDS_SOLID = 0,
		LDS_DASH = 1,
		LDS_DOT = 2,
		LDS_DASHDOT = 3,
		LDS_DASHDOTDOT = 4
	}

	public enum GRE_EDGETYPE
	{
		ET_SUNKEN = 1,
		ET_RAISED = 2
	}

	public enum GRE_BUTTONSTYLE
	{
		BT_NORMALWINDOWS = 0,
		BT_LIGHTWEIGHT = 1
	}

	public enum GRE_LINETYPE
	{
		LT_NORMAL = 0,
		LT_BORDER = 1,
		LT_FILLED = 2
	}

	public enum GRE_BACKGROUNDMODE
	{
		FP_SOLID = 0,
		FP_TRANSPARENT = 1,
		FP_GRADIENT = 2,
		FP_PATTERN = 3,
		FP_HATCH = 4
	}

	public enum GRE_PATTERN
	{
		FP_HORIZONTALLINE = 2,
		FP_VERTICALLINE = 3,
		FP_UPWARDDIAGONAL = 4,
		FP_DOWNWARDDIAGONAL = 5,
		FP_CROSS = 6,
		FP_DIAGONALCROSS = 7,
		FP_LIGHT = 8,
		FP_MEDIUM = 9,
		FP_DARK = 10
	}

	public enum GRE_HATCHSTYLE
	{
		HS_HORIZONTAL = 0,
		HS_VERTICAL = 1,
		HS_FORWARDDIAGONAL = 2,
		HS_BACKWARDDIAGONAL = 3,
		HS_LARGEGRID = 4,
		HS_DIAGONALCROSS = 5,
		HS_PERCENT05 = 6,
		HS_PERCENT10 = 7,
		HS_PERCENT20 = 8,
		HS_PERCENT25 = 9,
		HS_PERCENT30 = 10,
		HS_PERCENT40 = 11,
		HS_PERCENT50 = 12,
		HS_PERCENT60 = 13,
		HS_PERCENT70 = 14,
		HS_PERCENT75 = 15,
		HS_PERCENT80 = 16,
		HS_PERCENT90 = 17,
		HS_LIGHTDOWNWARDDIAGONAL = 18,
		HS_LIGHTUPWARDDIAGONAL = 19,
		HS_DARKDOWNWARDDIAGONAL = 20,
		HS_DARKUPWARDDIAGONAL = 21,
		HS_WIDEDOWNWARDDIAGONAL = 22,
		HS_WIDEUPWARDDIAGONAL = 23,
		HS_LIGHTVERTICAL = 24,
		HS_LIGHTHORIZONTAL = 25,
		HS_NARROWVERTICAL = 26,
		HS_NARROWHORIZONTAL = 27,
		HS_DARKVERTICAL = 28,
		HS_DARKHORIZONTAL = 29,
		HS_DASHEDDOWNWARDDIAGONAL = 30,
		HS_DASHEDUPWARDDIAGONAL = 31,
		HS_DASHEDHORIZONTAL = 32,
		HS_DASHEDVERTICAL = 33,
		HS_SMALLCONFETTI = 34,
		HS_LARGECONFETTI = 35,
		HS_ZIGZAG = 36,
		HS_WAVE = 37,
		HS_DIAGONALBRICK = 38,
		HS_HORIZONTALBRICK = 39,
		HS_WEAVE = 40,
		HS_PLAID = 41,
		HS_DIVOT = 42,
		HS_DOTTEDGRID = 43,
		HS_DOTTEDDIAMOND = 44,
		HS_SHINGLE = 45,
		HS_TRELLIS = 46,
		HS_SPHERE = 47,
		HS_SMALLGRID = 48,
		HS_SMALLCHECKERBOARD = 49,
		HS_LARGECHECKERBOARD = 50,
		HS_OUTLINEDDIAMOND = 51,
		HS_SOLIDDIAMOND = 52
	}

	public enum GRE_FIGURETYPE
	{
		FT_NONE = 0,
		FT_PROJECTUP = 1,
		FT_PROJECTDOWN = 2,
		FT_DIAMOND = 3,
		FT_CIRCLEDIAMOND = 4,
		FT_TRIANGLEUP = 5,
		FT_TRIANGLEDOWN = 6,
		FT_TRIANGLERIGHT = 7,
		FT_TRIANGLELEFT = 8,
		FT_CIRCLETRIANGLEUP = 9,
		FT_CIRCLETRIANGLEDOWN = 10,
		FT_ARROWUP = 11,
		FT_ARROWDOWN = 12,
		FT_CIRCLEARROWUP = 13,
		FT_CIRCLEARROWDOWN = 14,
		FT_SMALLPROJECTUP = 15,
		FT_SMALLPROJECTDOWN = 16,
		FT_RECTANGLE = 17,
		FT_SQUARE = 18,
		FT_CIRCLE = 19
	}

	public enum GRE_VERTICALALIGNMENT
	{
		VAL_TOP = 1,
		VAL_CENTER = 2,
		VAL_BOTTOM = 3
	}

	public enum GRE_HORIZONTALALIGNMENT
	{
		HAL_LEFT = 1,
		HAL_CENTER = 2,
		HAL_RIGHT = 3
	}

	public enum GRE_GRADIENTFILLMODE
	{
		GDT_HORIZONTAL = 0,
		GDT_VERTICAL = 1
	}

	public enum GRE_FILLMODE
	{
		FM_COMPLETELYFILLED = 0,
		FM_UPPERHALFFILLED = 1,
		FM_LOWERHALFFILLED = 2
	}

	public enum E_EVENTTARGET
	{
		EVT_NONE = 0,
		EVT_TASK = 1,
		EVT_PERCENTAGE = 2,
		EVT_MILESTONE = 3,
		EVT_ROW = 4,
		EVT_CELL = 5,
		EVT_COLUMN = 6,
		EVT_CLIENTAREA = 7,
		EVT_EMPTYAREA = 8,
		EVT_GRID = 9,
		EVT_TIMELINE = 10,
		EVT_TIMEBLOCK = 11,
		EVT_HSCROLLBAR = 12,
		EVT_TIMELINESCROLLBAR = 13,
		EVT_VSCROLLBAR = 14,
		EVT_TREEVIEW = 15,
		EVT_SPLITTER = 16,
		EVT_NODE = 17,
		EVT_TREEVIEWCHECKBOX = 19,
		EVT_TREEVIEWSIGN = 20,
		EVT_SELECTEDCOLUMN = 50,
		EVT_SELECTEDROW = 51,
		EVT_SELECTEDTASK = 52,
		EVT_SELECTEDPERCENTAGE = 54,
		EVT_PREDECESSOR = 56,
        EVT_SELECTEDPREDECESSOR = 57
	}

	public enum E_OPERATION
	{
		EO_NONE = 0,
    
		EO_TASKADDITION = 1,
		EO_TASKMOVEMENT = 2,
		EO_TASKSTRETCHLEFT = 3,
		EO_TASKSTRETCHRIGHT = 4,
		EO_TASKSELECTION = 5,
    
		EO_MILESTONEADDITION = 6,
    
		EO_ROWSIZING = 9,
		EO_ROWMOVEMENT = 10,
		EO_ROWSELECTION = 11,
    
		EO_COLUMNSIZING = 12,
		EO_COLUMNMOVEMENT = 13,
		EO_COLUMNSELECTION = 14,
    
		EO_TIMELINEMOVEMENT = 16,
    
		EO_SPLITTERMOVEMENT = 18,
    
		EO_HORIZONTALSCROLLBAR = 19,
		EO_VERTICALSCROLLBAR = 20,
		EO_TIMELINESCROLLBAR = 21,
		EO_TREEVIEW = 22,
    
		EO_PERCENTAGESELECTION = 23,
		EO_PERCENTAGESIZING = 24,
    
		////Treeview
		EO_CHECKBOXCLICK = 27,
		EO_SIGNCLICK = 28,
    
		////Predecessors
		EO_PREDECESSORADDITION = 29,
        EO_PREDECESSORSELECTION = 30
    
	}

	public enum E_TIERPOSITION
	{
		SP_UPPER = 0,
		SP_LOWER = 1,
		SP_MIDDLE = 2
	}

	public enum E_SORTTYPE
	{
		ES_STRING = 0,
		ES_NUMERIC = 1,
		ES_DATE = 2
	}

	public enum E_ADDMODE
	{
		AT_TASKADD = 0,
		AT_MILESTONEADD = 1,
		AT_BOTH = 2,
        AT_DURATION_TASKADD = 3,
        AT_DURATION_MILESTONEADD = 4,
        AT_DURATION_BOTH = 5
	}

	public enum E_OBJECTTYPE
	{
		OT_TASK = 0,
		OT_MILESTONE = 1
	}

	public enum E_PLACEMENT
	{
		PLC_ROWEXTENTSPLACEMENT = 0,
		PLC_OFFSETPLACEMENT = 1
	}

	public enum E_TEXTPLACEMENT
	{
		SCP_OBJECTEXTENTSPLACEMENT = 0,
		SCP_OFFSETPLACEMENT = 1,
		SCP_EXTERIORPLACEMENT = 2
	}

	public enum E_STYLEAPPEARANCE
	{
		SA_RAISED = 0,
		SA_SUNKEN = 1,
		SA_FLAT = 2,
		SA_GRAPHICAL = 3,
		SA_CELL = 4
	}

	public enum E_SCROLLBEHAVIOUR
	{
		SB_DISABLE = 0,
		SB_HIDE = 1
	}

	public enum E_REPORTERRORS
	{
		RE_MSGBOX = 0,
		RE_RAISE = 1,
		RE_RAISEEVENT = 2,
		RE_HIDE = 3
	}

	public enum E_PROGRESSLINELENGTH
	{
		TLMA_TICKMARKAREA = 0,
		TLMA_CLIENTAREA = 1,
		TLMA_BOTH = 2,
		TLMA_NONE = 4
	}

	public enum GRE_BORDERSTYLE
	{
		SBR_NONE = 0,
		SBR_SINGLE = 1,
		SBR_CUSTOM = 2
	}

	public enum E_BORDERSTYLE
	{
		TLB_NONE = 0,
		TLB_SINGLE = 1,
		TLB_3D = 2
	}

	public enum E_TIERTYPE
	{
		ST_DAYOFWEEK = 1,
		ST_MONTH = 2,
		ST_QUARTER = 3,
		ST_YEAR = 4,
		ST_WEEK = 5,
		ST_CUSTOM = 6,
		ST_DAY = 7,
		ST_DAYOFYEAR = 8,
		ST_HOUR = 9,
		ST_MINUTE = 10,
        ST_SECOND = 11,
        ST_MILLISECOND = 12,
        ST_MICROSECOND = 13
	}

	public enum E_PROGRESSLINETYPE
	{
		TLMT_SYSTEMTIME = 0,
		TLMT_USER = 1
	}

	public enum E_TIMEBLOCKBEHAVIOUR
	{
		TBB_ROWEXTENTS = 0,
		TBB_CONTROLEXTENTS = 1
	}

	public enum E_LAYEROBJECTENABLE
	{
		EC_INCURRENTLAYERONLY = 0,
		EC_INALLLAYERS = 1
	}

	public enum E_MOVEMENTTYPE
	{
		MT_UNRESTRICTED = 0,
		MT_RESTRICTEDTOROW = 1,
		MT_MOVEMENTDISABLED = 2
	}

	public enum E_TICKMARKTYPES
	{
		TLT_BIG = 0,
		TLT_MEDIUM = 1,
		TLT_SMALL = 2
	}

	public enum E_SCROLLBAR
	{
		SCR_VERTICAL = 0,
		SCR_HORIZONTAL1 = 1,
		SCR_HORIZONTAL2 = 2
	}

	public enum E_CONTROLMODE
	{
		CM_GRID = 0,
		CM_TREEVIEW = 1
	}

	public enum E_CONSTRAINTTYPE
	{
		PCT_END_TO_START = 0,
		PCT_START_TO_START = 1,
		PCT_END_TO_END = 2,
		PCT_START_TO_END = 3
	}

	public enum E_TIMEBLOCKTYPE
	{
		TBT_SINGLE_OCURRENCE = 0,
		TBT_RECURRING = 1
	}

	public enum E_RECURRINGTYPE
	{
        RCT_DAY = 3,
        RCT_WEEK = 4,
        RCT_MONTH = 5,
        RCT_YEAR = 7
	}

	public enum E_WEEKDAY
	{
		WD_SUNDAY = 1,
		WD_MONDAY = 2,
		WD_TUESDAY = 3,
		WD_WEDNESDAY = 4,
		WD_THURSDAY = 5,
		WD_FRIDAY = 6,
		WD_SATURDAY = 7
	}

	public enum E_TOOLTIPTYPE
	{
		TPT_HOVER = 0,
		TPT_MOVEMENT = 1
	}

	public enum E_MOUSEBUTTONS
	{
		BTN_NONE = 0,
		BTN_LEFT = 1048576,
		BTN_RIGHT = 2097152,
		BTN_MIDDLE = 4194304,
		BTN_XBUTTON1 = 8388608,
		BTN_XBUTTON2 = 16777216
	}

    public enum E_INTERVAL
    {
        IL_NANOSECOND = -3,
        IL_MICROSECOND = -2,
        IL_MILLISECOND = -1,
        IL_SECOND = 0,
        IL_MINUTE = 1,
        IL_HOUR = 2,
        IL_DAY = 3,
        IL_WEEK = 4,
        IL_MONTH = 5,
        IL_QUARTER = 6,
        IL_YEAR = 7
    }

    public enum E_SPLITTERTYPE
    {
        SA_APPEARANCE = 1,
        SA_USERDEFINED = 2,
        SA_STYLE = 3
    }

    public enum E_TIERBACKGROUNDMODE
    {
        ETBGM_TIERAPPEARANCE = 0,
        ETBGM_STYLE = 2
    }

    public enum E_TIERAPPEARANCESCOPE
    {
        TAS_CONTROL = 0,
        TAS_VIEW = 1
    }

    public enum E_TIERFORMATSCOPE
    {
        TFS_CONTROL = 0,
        TFS_VIEW = 1
    }

    public enum E_SELECTIONRECTANGLEMODE
    {
        SRM_DOTTED = 0,
        SRM_COLOR = 1
    }

    public enum E_PREDECESSORMODE
    {
        PM_FORCE = 0,
        PM_CREATEWARNINGFLAG = 1,
        PM_RAISEEVENT = 2
    }

    public enum E_TASKTYPE
    {
        TT_START_END = 0,
        TT_DURATION = 1,
        TT_UNITS_DURATION_WORK = 2
    }

    public enum E_TBINTERVALTYPE
    {
        TBIT_AUTOMATIC = 0,
        TBIT_MANUAL = 1
    }

	public enum SYS_ERRORS
	{
		ERR_RETARRELEMKEY_G = 51132,
		ERR_COLLREMWHERE_1_G = 51133,
		ERR_COLLREMWHERE_2_G = 51134,
		ERR_COLLREMWHERE_3_G = 51135,
		ERR_COLLREMWHERE_4_G = 51136,
		ERR_COLLREMWHERENOT_1_G = 51137,
		ERR_COLLREMWHERENOT_2_G = 51138,
		ERR_COLLREMWHERENOT_3_G = 51139,
		ERR_COLLREMWHERENOT_4_G = 51140,
		ERR_ADDMODE_G = 51141,
		TASKS_ITEM_1 = 51142,
		TASKS_ITEM_2 = 51143,
		TASKS_ITEM_3 = 51144,
		TASKS_ITEM_4 = 51145,
		TASKS_ADD_1 = 51146,
		TASKS_ADD_2 = 51147,
		TASKS_ADD_3 = 51148,
		TASKS_REMOVE_1 = 51149,
		TASKS_REMOVE_2 = 51150,
		TASKS_REMOVE_3 = 51151,
		TASKS_REMOVE_4 = 51152,
		TASKS_SET_KEY = 51153,
		ROWS_ITEM_1 = 51155,
		ROWS_ITEM_2 = 51156,
		ROWS_ITEM_3 = 51157,
		ROWS_ITEM_4 = 51158,
		ROWS_ADD_1 = 51159,
		ROWS_ADD_2 = 51160,
		ROWS_ADD_3 = 51161,
		ROWS_REMOVE_1 = 51162,
		ROWS_REMOVE_2 = 51163,
		ROWS_REMOVE_3 = 51164,
		ROWS_REMOVE_4 = 51165,
		ROWS_SET_KEY = 51166,
		COLUMNS_ITEM_1 = 51168,
		COLUMNS_ITEM_2 = 51169,
		COLUMNS_ITEM_3 = 51170,
		COLUMNS_ITEM_4 = 51171,
		COLUMNS_ADD_1 = 51172,
		COLUMNS_ADD_2 = 51173,
		COLUMNS_ADD_3 = 51174,
		COLUMNS_REMOVE_1 = 51175,
		COLUMNS_REMOVE_2 = 51176,
		COLUMNS_REMOVE_3 = 51177,
		COLUMNS_REMOVE_4 = 51178,
		COLUMNS_SET_KEY = 51179,
		CELLS_ITEM_1 = 51181,
		CELLS_ITEM_2 = 51182,
		CELLS_ITEM_3 = 51183,
		CELLS_ITEM_4 = 51184,
		CELLS_ADD_1 = 51185,
		CELLS_ADD_2 = 51186,
		CELLS_ADD_3 = 51187,
		CELLS_REMOVE_1 = 51188,
		CELLS_REMOVE_2 = 51189,
		CELLS_REMOVE_3 = 51190,
		CELLS_REMOVE_4 = 51191,
		CELLS_SET_KEY = 51192,
		PREDECESSORS_ITEM_1 = 51194,
		PREDECESSORS_ITEM_2 = 51195,
		PREDECESSORS_ITEM_3 = 51196,
		PREDECESSORS_ITEM_4 = 51197,
		PREDECESSORS_ADD_1 = 51198,
		PREDECESSORS_ADD_2 = 51199,
		PREDECESSORS_ADD_3 = 51200,
		PREDECESSORS_REMOVE_1 = 51201,
		PREDECESSORS_REMOVE_2 = 51202,
		PREDECESSORS_REMOVE_3 = 51203,
		PREDECESSORS_REMOVE_4 = 51204,
		PREDECESSORS_SET_KEY = 51205,
		TIMEBLOCKS_ITEM_1 = 51207,
		TIMEBLOCKS_ITEM_2 = 51208,
		TIMEBLOCKS_ITEM_3 = 51209,
		TIMEBLOCKS_ITEM_4 = 51210,
		TIMEBLOCKS_ADD_1 = 51211,
		TIMEBLOCKS_ADD_2 = 51212,
		TIMEBLOCKS_ADD_3 = 51213,
		TIMEBLOCKS_REMOVE_1 = 51214,
		TIMEBLOCKS_REMOVE_2 = 51215,
		TIMEBLOCKS_REMOVE_3 = 51216,
		TIMEBLOCKS_REMOVE_4 = 51217,
		TIMEBLOCKS_SET_KEY = 51218,
		LAYERS_ITEM_1 = 51220,
		LAYERS_ITEM_2 = 51221,
		LAYERS_ITEM_3 = 51222,
		LAYERS_ITEM_4 = 51223,
		LAYERS_ADD_1 = 51224,
		LAYERS_ADD_2 = 51225,
		LAYERS_ADD_3 = 51226,
		LAYERS_REMOVE_1 = 51227,
		LAYERS_REMOVE_2 = 51228,
		LAYERS_REMOVE_3 = 51229,
		LAYERS_REMOVE_4 = 51230,
		LAYERS_SET_KEY = 51231,
		STYLES_ITEM_1 = 51233,
		STYLES_ITEM_2 = 51234,
		STYLES_ITEM_3 = 51235,
		STYLES_ITEM_4 = 51236,
		STYLES_ADD_1 = 51237,
		STYLES_ADD_2 = 51238,
		STYLES_ADD_3 = 51239,
		STYLES_REMOVE_1 = 51240,
		STYLES_REMOVE_2 = 51241,
		STYLES_REMOVE_3 = 51242,
		STYLES_REMOVE_4 = 51243,
		STYLES_SET_KEY = 51244,
		PERCENTAGES_ITEM_1 = 51246,
		PERCENTAGES_ITEM_2 = 51247,
		PERCENTAGES_ITEM_3 = 51248,
		PERCENTAGES_ITEM_4 = 51249,
		PERCENTAGES_ADD_1 = 51250,
		PERCENTAGES_ADD_2 = 51251,
		PERCENTAGES_ADD_3 = 51252,
		PERCENTAGES_REMOVE_1 = 51253,
		PERCENTAGES_REMOVE_2 = 51254,
		PERCENTAGES_REMOVE_3 = 51255,
		PERCENTAGES_REMOVE_4 = 51256,
		PERCENTAGES_SET_KEY = 51257,
		VIEWS_ITEM_1 = 51272,
		VIEWS_ITEM_2 = 51273,
		VIEWS_ITEM_3 = 51274,
		VIEWS_ITEM_4 = 51275,
		VIEWS_ADD_1 = 51276,
		VIEWS_ADD_2 = 51277,
		VIEWS_ADD_3 = 51278,
		VIEWS_REMOVE_1 = 51279,
		VIEWS_REMOVE_2 = 51280,
		VIEWS_REMOVE_3 = 51281,
		VIEWS_REMOVE_4 = 51282,
		VIEWS_SET_KEY = 51283,
		TIERCOLORS_ITEM_1 = 51285,
		TIERCOLORS_ITEM_2 = 51286,
		TIERCOLORS_ITEM_3 = 51287,
		TIERCOLORS_ITEM_4 = 51288,
		TIERCOLORS_ADD_1 = 51289,
		TIERCOLORS_ADD_2 = 51290,
		TIERCOLORS_ADD_3 = 51291,
		TIERCOLORS_REMOVE_1 = 51292,
		TIERCOLORS_REMOVE_2 = 51293,
		TIERCOLORS_REMOVE_3 = 51294,
		TIERCOLORS_REMOVE_4 = 51295,
		TIERCOLORS_SET_KEY = 51296,
		TICKMARKS_ITEM_1 = 51298,
		TICKMARKS_ITEM_2 = 51299,
		TICKMARKS_ITEM_3 = 51300,
		TICKMARKS_ITEM_4 = 51301,
		TICKMARKS_ADD_1 = 51302,
		TICKMARKS_ADD_2 = 51303,
		TICKMARKS_ADD_3 = 51304,
		TICKMARKS_REMOVE_1 = 51305,
		TICKMARKS_REMOVE_2 = 51306,
		TICKMARKS_REMOVE_3 = 51307,
		TICKMARKS_REMOVE_4 = 51308,
		TICKMARKS_SET_KEY = 51309,
		INVALID_INTERVAL = 51310,
		INVALID_LAYER_INDEX = 51311,
		GETINDEXANDKEY_ITEM1 = 51312,
		GETINDEXANDKEY_ITEM2 = 51313,
		GETINDEXANDKEY_ITEM3 = 51314,
		GETINDEXANDKEY_ITEM4 = 51315,
		INVALID_ROW_KEY = 51316,
		STYLE_INVALID_INDEX = 51317,
		STYLE_INVALID_KEY = 51318,
        MP_REMOVE_1 = 51596,
        MP_REMOVE_2 = 51597,
        MP_REMOVE_3 = 51598,
        MP_REMOVE_4 = 51599,
        MP_ITEM_1 = 51600,
        MP_ITEM_2 = 51601,
        MP_ITEM_3 = 51602,
        MP_ITEM_4 = 51603,
        MP_ADD_1 = 51604,
        MP_ADD_2 = 51605,
        MP_ADD_3 = 51606,
        MP_SET_KEY = 51607,
        SPLITTER_INVALIDOP = 51608,
        SPLITTER_INVALID_INDEX = 51609,
        SPLITTER_INVALID_WIDTH = 51610,
        STYLE_NULL = 51611,
        INVALID_DURATION_INTERVAL = 51612,
        CHECK_DURATION_ERROR = 51613,
        ERR_DURATION_INCONSISTENT = 51614
	}

	internal enum E_SCROLLSTATE
	{
		SS_CANTDISPLAY = 0,
		SS_NOTNEEDED = 1,
		SS_NEEDED = 2,
		SS_SHOWN = 3,
		SS_HIDDEN = 4
	}

	internal enum E_AREA
	{
		EA_NONE = 0,
		EA_LEFT = 1,
		EA_TOP = 2,
		EA_RIGHT = 3,
		EA_BOTTOM = 4,
		EA_CENTER = 5
	}

	internal enum E_FOCUSTYPE
	{
		FCT_NORMAL = 0,
		FCT_KEEPLEFTRIGHTBOUNDS = 1,
		FCT_ADD = 2,
		FCT_VERTICALSPLITTER = 3
	}


}

