using System.Collections.Generic;
using UnityEngine;

namespace AutoBattle
{
    public class BattleLayer : MonoBehaviour
    {
        /** ユニットPrefab */
        [SerializeField] public GameObject unitViewPrefab;
        /** 味方親 */
        [SerializeField] public Transform unitPrent;
        /** 敵親 */
        [SerializeField] public Transform enemyParent;

        private readonly List<UnitView> _allUnitViews = new();
        private readonly List<UnitView> _enemyUnitViews = new();
        
        private const float UnitSpacing = 30f; 
        
        // ユニットビュー追加
        public void AddUnitView(Unit unit, bool isAlly)
        {
            var unitView = Instantiate(unitViewPrefab, isAlly ? unitPrent : enemyParent).GetComponent<UnitView>();
            unitView.SetUp(unit.Belonging, unit.Index, unit.Name, unit.MaxHp, unit.Type);
            
            var unitsList = isAlly ? _allUnitViews : _enemyUnitViews;
            unitsList.Add(unitView);

            // 初期位置設定
            var rectTransform = (RectTransform)unitView.transform;
            var width = rectTransform.rect.width;
            rectTransform.anchoredPosition = new Vector2((width + UnitSpacing) * (unit.Index - 1), 0);
        }
        
        // ユニットビュー取得
        public UnitView GetUnitView(Unit unit)
        {
            var unitsList = unit.Belonging == Belonging.Ally ? _allUnitViews : _enemyUnitViews;
            return unitsList.Find(view => view.Index == unit.Index);
        }
    }
}