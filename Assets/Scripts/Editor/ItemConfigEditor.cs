using UnityEditor;
using Config;

[CustomEditor(typeof(ItemConfig))]
public class ItemConfigEditor : Editor
{
    private ItemConfig _item;
    private SerializedProperty _isStackable;
    private SerializedProperty _stackLimit;
    private SerializedProperty _isUsable;
    private SerializedProperty _usageType;
    private SerializedProperty _usageEffectType;
    private SerializedProperty _usageEffectCount;
    private SerializedProperty _isCraftable;
    private SerializedProperty _ingredients;
    private SerializedProperty _craftedItemCount;

    private void OnEnable()
    {
        _item = (ItemConfig)target;
        _isStackable = serializedObject.FindProperty(nameof(_item.IsStackable));
        _stackLimit = serializedObject.FindProperty(nameof(_item.StackLimit));
        _isUsable = serializedObject.FindProperty(nameof(_item.IsUsable));
        _usageType = serializedObject.FindProperty(nameof(_item.UsageType));
        _usageEffectType = serializedObject.FindProperty(nameof(_item.UsageEffectType));
        _usageEffectCount = serializedObject.FindProperty(nameof(_item.UsageEffectCount));
        _isCraftable = serializedObject.FindProperty(nameof(_item.IsCraftable));
        _ingredients = serializedObject.FindProperty(nameof(_item.Ingredients));
        _craftedItemCount = serializedObject.FindProperty(nameof(_item.CraftedItemCount));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();

        EditorGUILayout.PropertyField(_isStackable);
        if (_isStackable.boolValue)
            EditorGUILayout.PropertyField(_stackLimit);
        EditorGUILayout.PropertyField(_isUsable);
        if (_isUsable.boolValue)
        {
            EditorGUILayout.PropertyField(_usageType);
            switch (_usageType.enumValueFlag)
            {
                case (int)ItemUsageType.Consumable:
                    EditorGUILayout.PropertyField(_usageEffectType);
                    EditorGUILayout.PropertyField(_usageEffectCount);
                    break;
            }
        }
        EditorGUILayout.PropertyField(_isCraftable);
        if (_isCraftable.boolValue)
        {
            EditorGUILayout.PropertyField(_ingredients);
            EditorGUILayout.PropertyField(_craftedItemCount);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
