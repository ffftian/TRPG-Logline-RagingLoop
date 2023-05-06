using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
public class FgiPopup
{
    public Action<FgiData> OnPopupChange;

    private string popupName;
    private List<FgiData> fgiDatas;

    private string[] casheFgis;
    private int Index;

    public FgiPopup(string popupName, List<FgiData> fgiDatas, Action<FgiData> onPopupChange)
    {
        this.popupName = popupName;
        this.fgiDatas = fgiDatas;
        casheFgis = fgiDatas.Select(a => a.name).ToArray();
        OnPopupChange = onPopupChange;
    }

    public void OnInspectorGUI()
    {
        int cashe = Index;
        Index = EditorGUILayout.Popup(popupName, Index, casheFgis);
        if (cashe != Index)
        {
            OnPopupChange.Invoke(fgiDatas[Index]);
        }
    }
}

