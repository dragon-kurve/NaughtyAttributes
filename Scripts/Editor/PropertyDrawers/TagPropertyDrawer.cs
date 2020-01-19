﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NaughtyAttributes.Editor
{
	[CustomPropertyDrawer(typeof(TagAttribute))]
	public class TagPropertyDrawer : PropertyDrawerBase
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return (property.propertyType == SerializedPropertyType.String)
				? GetPropertyHeight(property)
				: GetPropertyHeight(property) + GetHelpBoxHeight();
		}

		protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType == SerializedPropertyType.String)
			{
				// generate the taglist + custom tags
				List<string> tagList = new List<string>();
				tagList.Add("(None)");
				tagList.Add("Untagged");
				tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);

				string propertyString = property.stringValue;
				int index = 0;
				// check if there is an entry that matches the entry and get the index
				// we skip index 0 as that is a special custom case
				for (int i = 1; i < tagList.Count; i++)
				{
					if (tagList[i] == propertyString)
					{
						index = i;
						break;
					}
				}

				// Draw the popup box with the current selected index
				index = EditorGUI.Popup(rect, label.text, index, tagList.ToArray());

				// Adjust the actual string value of the property based on the selection
				if (index > 0)
				{
					property.stringValue = tagList[index];
				}
				else
				{
					property.stringValue = string.Empty;
				}
			}
			else
			{
				string message = string.Format("{0} supports only string fields", typeof(TagAttribute).Name);
				DrawDefaultPropertyAndHelpBox(rect, property, message, MessageType.Warning);
			}
		}
	}
}
