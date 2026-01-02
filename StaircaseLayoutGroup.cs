namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("Layout/Staircase Vertical Layout Group")]
    public class StaircaseVerticalLayoutGroup : HorizontalOrVerticalLayoutGroup
    {
        [SerializeField] protected float m_StaircaseOffset = 20f;

        public float staircaseOffset
        {
            get { return m_StaircaseOffset; }
            set { SetProperty(ref m_StaircaseOffset, value); }
        }

        protected StaircaseVerticalLayoutGroup()
        { }

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            float combinedPadding = padding.horizontal;
            float totalMin = combinedPadding;
            float totalPreferred = combinedPadding;
            float totalFlexible = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                RectTransform child = rectChildren[i];
                float min = LayoutUtility.GetMinSize(child, 0);
                float preferred = LayoutUtility.GetPreferredSize(child, 0);
                float flexible = LayoutUtility.GetFlexibleSize(child, 0);

                if (childForceExpandWidth)
                    flexible = Mathf.Max(flexible, 1);

                float currentOffset = i * m_StaircaseOffset;

                totalMin = Mathf.Max(min + combinedPadding + Mathf.Abs(currentOffset), totalMin);
                totalPreferred = Mathf.Max(preferred + combinedPadding + Mathf.Abs(currentOffset), totalPreferred);
                totalFlexible = Mathf.Max(flexible, totalFlexible);
            }

            SetLayoutInputForAxis(totalMin, totalPreferred, totalFlexible, 0);
        }

        public override void CalculateLayoutInputVertical()
        {
            CalcAlongAxis(1, true);
        }

        public override void SetLayoutHorizontal()
        {
            float size = rectTransform.rect.width;
            float innerSize = size - padding.horizontal;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                RectTransform child = rectChildren[i];
                float min = LayoutUtility.GetMinSize(child, 0);
                float preferred = LayoutUtility.GetPreferredSize(child, 0);
                float flexible = LayoutUtility.GetFlexibleSize(child, 0);

                if (childForceExpandWidth)
                    flexible = Mathf.Max(flexible, 1);

                float requiredSpace = Mathf.Clamp(innerSize, min, flexible > 0 ? size : preferred);

                float startOffset = GetStartOffset(0, requiredSpace);

                float stepOffset = i * m_StaircaseOffset;

                SetChildAlongAxis(child, 0, startOffset + stepOffset, requiredSpace);
            }
        }

        public override void SetLayoutVertical()
        {
            SetChildrenAlongAxis(1, true);
        }
    }
}