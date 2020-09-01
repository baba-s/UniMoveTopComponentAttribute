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
			ObjectFactory.componentWasAdded += OnComponentWasAdded;
			Selection.selectionChanged      += OnSelectionChanged;
		}

		private static void OnComponentWasAdded( Component component )
		{
			Move( component );
		}

		private static void OnSelectionChanged()
		{
			var monoBehaviours = Selection.gameObjects
					.SelectMany( x => x.GetComponents<MonoBehaviour>() )
					.ToArray()
				;

			foreach ( var monoBehaviour in monoBehaviours )
			{
				Move( monoBehaviour );
			}
		}

		private static void Move( Component component )
		{
			var type       = component.GetType();
			var attributes = type.GetCustomAttributes( ATTRIBUTE_TYPE, true );

			if ( attributes.Length <= 0 ) return;

			while ( ComponentUtility.MoveComponentUp( component ) )
			{
			}
		}
	}
}