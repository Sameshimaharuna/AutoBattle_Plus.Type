using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AutoBattle
{
    public class UnitView : MonoBehaviour
    {
        /** 名前 */
        [SerializeField] private Text nameText;
        /** hpバー */
        [SerializeField] private Image hpBar;
        /** ユニット画像 */
        [SerializeField] private Image unitImage;

        /** 所属 */
        private Belonging _belonging;
        /** index */
        public int Index { get; private set; }
        /** 最大HP */
        private int _maxHp;
        /** 追加：属性 */
        private Type _type;

        /** 初期化 */
        public void SetUp(Belonging belonging, int index, string unitName, int maxHp, Type type)
        {
            _belonging = belonging;
            Index = index;
            nameText.text = unitName;
            _maxHp = maxHp;
            // 追加：属性
            _type = type;
            TypeColor();
        }

        /** 追加：名前＋属性色 */
        public void TypeColor()
        {
            if (_type == Type.Fire)
            {
                nameText.color = new Color(255f, 0f, 0f);
            }

            else if (_type == Type.Water)
            {
                nameText.color = new Color(0f, 0f, 255f);
            }

            else if (_type == Type.Grass)
            {
                nameText.color = new Color(0f, 255f, 0f);
            }
        }

        /** 攻撃モーション */
        public async UniTask AttackMotionAsync()
        {
            var attackDistance = 50f * (_belonging == Belonging.Ally ? 1 : -1);
            // 攻撃モーション
            await DOTween.Sequence().SetRelative()
                .Append(transform.DOLocalMoveY(attackDistance, 0.2f).SetEase(Ease.InSine))
                .AppendInterval(0.3f)
                .Append(transform.DOLocalMoveY(-attackDistance, 0.2f).SetEase(Ease.InSine))
                .AsyncWaitForCompletion().AsUniTask();
        }
        
        /** ダメージモーション */
        public async UniTask DamageMotionAsync(int nextHp)
        {
            hpBar.fillAmount = (float)nextHp / _maxHp;
            // 横に揺れる
            await transform.DOPunchPosition(Vector2.right * 30f, 1f, 5)
                .AsyncWaitForCompletion().AsUniTask();
            if (nextHp <= 0)
            {
                // 死亡時の処理
                unitImage.color = Color.gray;
            }
        }
        
        
    }
}