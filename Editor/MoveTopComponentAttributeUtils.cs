using System;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Kogane.Internal
{
	[InitializeOnLoad]
	internal static class MoveTopComponentAttributeUtils
	{
		private static readonly Type ATTRIBUTE_TYPE = typeof( MoveTopComponentAttribute );

		static MoveTopComponentAttributeUtils()
		{
			Selection.selectionChanged += OnSelectionChanged;
		}

		private static void OnSelectionChanged()
		{
			var monoBehaviours = Selection.gameObjects
					.SelectMany( x => x.GetComponents<MonoBehaviour>() )
					.ToArray()
				;

			foreach ( var monoBehaviour in monoBehaviours )
			{
				var type       = monoBehaviour.GetType();
				var attributes = type.GetCustomAttributes( ATTRIBUTE_TYPE, true );

				if ( attributes.Length <= 0 ) continue;

				while ( ComponentUtility.MoveComponentUp( monoBehaviour ) )
				{
				}
			}
		}
	}
}