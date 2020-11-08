﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Azirel.View
{
	public interface ISignInViewBase
	{
		event Action OnSignInButtonPressed;
		IEnumerable<Tuple<string, string>> MainInfo { set; }
		Texture MainImageProvider { set; }
	}

	public class SignInViewControllerBase : MonoBehaviour, ISignInViewBase
	{
		[SerializeField] protected Button _signInButton;
		[SerializeField] protected StringsPairViewControllerPrefab _stringPairPrefab;
		[SerializeField] protected Transform _stringPairPrefabParent;
		[SerializeField] protected RawImage _mainImage;

		protected IDictionary<Tuple<string, string>, StringsPairViewControllerPrefab> _currentStringPairs = new Dictionary<Tuple<string, string>, StringsPairViewControllerPrefab>();

		public IEnumerable<Tuple<string, string>> MainInfo
		{
			set
			{
				FlushCurrentStringPairViews();
				CreateTextPairViews(value);
			}
		}

		public Texture MainImageProvider
		{
			set => _mainImage.texture = value;
		}

		public event Action OnSignInButtonPressed;

		protected virtual void CreateTextPairViews(IEnumerable<Tuple<string, string>> stringPairs)
		{
			foreach (var stringPair in stringPairs)
			{
				var pairInstance = Instantiate(_stringPairPrefab, _stringPairPrefabParent);
				pairInstance.Value = stringPair;
				_currentStringPairs.Add(stringPair, pairInstance);
			}
		}

		protected virtual void FlushCurrentStringPairViews()
		{
			foreach (var pair in _currentStringPairs)
				Destroy(pair.Value.gameObject);
			_currentStringPairs.Clear();
		}
	} 
}