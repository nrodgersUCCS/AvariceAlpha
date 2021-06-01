using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// A custom inspector class for the game constants
/// </summary>
[CustomEditor(typeof(Constants))]
[CanEditMultipleObjects]
public class ConstantsEditor : Editor
{
    private Constants values;           // Target Constants object
    GUIStyle headerStyle;               // Inspector header style

    // Used for collapsing fields
    private SerializedProperty currentProperty = null;
    private bool enemiesOpen = false;
    private bool globalEnemiesOpen = false;
    private bool weaponsOpen = false;
    private bool decorationsOpen = false;
    private bool armorOpen = false;
    private bool itemsOpen = false;

    /// <summary>
    /// Used for displaying inspector GUI
    /// </summary>
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        List<string> propertyNames = new List<string>();    // List used for filling groups of foldout properties

        #region Player Values

        // Player values
        EditorGUILayout.Space(5);
        ShowSubClass("Player", "Player Values");

        #endregion Player Values

        #region Enemy Values

        // Enemy values
        EditorGUILayout.Space(10);
        enemiesOpen = EditorGUILayout.Foldout(enemiesOpen, "Enemy Values");
        if (enemiesOpen)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.Space(5);

            // Global enemy values
            globalEnemiesOpen = EditorGUILayout.Foldout(globalEnemiesOpen, "Global Enemy Values");
            if (globalEnemiesOpen)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(serializedObject.FindProperty("Enemy").FindPropertyRelative("redFlashOpacity"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Enemy").FindPropertyRelative("redFlashTime"));

                EditorGUI.indentLevel--;
                EditorGUILayout.Space(10);
            }

            // Default enemy values
            ShowSubClass("Enemy", "Default Enemy Values");

            // List of enemy values to include
            propertyNames = new List<string>()
            {
                "AlgorMortis",
                "Cherub",
                "Bandit",
                "Skelodevil",
                "Hermit",
                "CorpseEater",
                "Shadowblade",
                "Brute",
                "IronMaidenTrap",
                "DemonKing"
            };

            // Show all enemy values
            foreach (string s in propertyNames)
            {
                ShowSubClass(s);
            }

            EditorGUI.indentLevel--;
        }

        #endregion Enemy Values

        #region Global Item Values

        // Global item values
        EditorGUILayout.Space(10);
        itemsOpen = EditorGUILayout.Foldout(itemsOpen, "Global Item Values");
        if (itemsOpen)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(serializedObject.FindProperty("Item").FindPropertyRelative("minPickupDistance"));

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("HUD Values");
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Item").FindPropertyRelative("minStackDistance"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Item").FindPropertyRelative("maxStackDistance"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Item").FindPropertyRelative("stackDistanceAmount"));

            EditorGUI.indentLevel--;
            EditorGUILayout.Space(10);
        }

        #endregion Global Item Values

        #region Weapon Values

        // Weapon Values
        EditorGUILayout.Space(10);
        weaponsOpen = EditorGUILayout.Foldout(weaponsOpen, "Weapon Values");
        if (weaponsOpen)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.Space(5);

            // Default throwable item values
            ShowSubClass("ThrowableItem", "Default Throwable Item Values");

            ShowSubClass("Weapon", "Default Weapon Values");

            // Weapon variant values
            ShowSubClass("WeaponVariant", "Weapon Variant Values");

            // List of weapon values to include
            propertyNames = new List<string>()
            {
                "Dagger",
                "Hoarfrost",
                "Spear",
                "Warhammer"
            };

            // Show all weapon values
            foreach (string s in propertyNames)
            {
                ShowSubClass(s);
            }

            // Decoration Values
            EditorGUILayout.Space(5);
            decorationsOpen = EditorGUILayout.Foldout(decorationsOpen, "Decoration Values");
            if (decorationsOpen)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space(5);

                // List of decoration values to include
                propertyNames = new List<string>()
                {
                    "BigCrate",
                    "SmallCrate",
                    "Barrel",
                    "CandleHolder",
                    "Table",
                    "IronMaiden",
                    "Plate",
                    "Fork",
                    "Knife",
                    "Chair"
                };

                // Show all decoration values
                foreach (string s in propertyNames)
                {
                    ShowSubClass(s);
                }

                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
        }

        #endregion Weapon Values

        #region Armor Values

        // Armor Values
        EditorGUILayout.Space(10);
        armorOpen = EditorGUILayout.Foldout(armorOpen, "Armor Values");
        if (armorOpen)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.Space(5);

            // Default armor values
            ShowSubClass("Armor", "Default Armor Values");

            // List of amor values to include
            propertyNames = new List<string>()
            {
                "LightArmor",
                "ChainMail",
                "PlateArmor"
            };

            // Show all armor values
            foreach (string s in propertyNames)
            {
                ShowSubClass(s);
            }

            EditorGUI.indentLevel--;
        }

        #endregion Armor Values

        #region Map Generation Values

        EditorGUILayout.Space(10);
        ShowSubClass("Map", "Map Values");

        #endregion Map Generation Values

        #region Loot Values

        EditorGUILayout.Space(10);
        ShowSubClass("Loot", "Loot Values");

        #endregion Loot Values

        // Apply changes
        serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Displays a constant sub-class in the inspector
    /// </summary>
    /// <param name="propertyName">The name of the property in Constants</param>
    /// <param name="altLabel">Optional alternate label for the foldout group</param>
    private void ShowSubClass(string propertyName, string altLabel = null)
    {
        // Get current constants property
        currentProperty = serializedObject.FindProperty(propertyName);

        // Create the field for the property
        if (altLabel != null)
            EditorGUILayout.PropertyField(currentProperty, new GUIContent(altLabel));
        else
            EditorGUILayout.PropertyField(currentProperty);

        // Add space in inspector when group is opened
        if (currentProperty.isExpanded)
            EditorGUILayout.Space(10);
    }
}