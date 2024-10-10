using UnityEngine;
using UnityEngine.UIElements;
public partial class AdaptiveText : Label
{
        public float minFontSize = 12;
        public float maxFontSize = 110;
        public float maxWidth = 600;
        public void OnEnable()
        { 
            //parent.RegisterCallback<GeometryChangedEvent>(evt => UpdateFontSize());
            RegisterCallback<GeometryChangedEvent>(evt => UpdateFontSize());
    }
        void UpdateFontSize()
        {
            float parentWidth = resolvedStyle.width;
            float parentHeight = resolvedStyle.height;
        
           // Рассчитываем новый размер шрифта на основе размеров контейнера
           float newFontSize = Mathf.Lerp(minFontSize, maxFontSize, parentWidth / maxWidth);

            // Устанавливаем размер шрифта
            style.fontSize = newFontSize;
            //Debug.Log($"{parentWidth}, {parentHeight}, {style.fontSize}, {newFontSize}");
        }
}