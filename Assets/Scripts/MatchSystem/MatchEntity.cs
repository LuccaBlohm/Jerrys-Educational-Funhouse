using UnityEngine;

public class MatchEntity : MonoBehaviour
{
    public MatchFeedback _feedback;
    public MovablePair _movablePair;
    public Renderer _fixedPairRenderer;
    public MatchSystem _matchSystem;

    private bool _matched;

    public Vector3 GetMovablePairPosition()
    {
        return _movablePair.GetPosition();
    }

    public void SetMovablePairPosition(Vector3 NewMovablePairPosition)
    {
        _movablePair.SetInitialPosition(NewMovablePairPosition);
    }

    public void SetMaterialToPairs(Material PairMaterial)
    {
        _movablePair.GetComponent<Renderer>().material = PairMaterial;
        _fixedPairRenderer.material = PairMaterial;
    }

    public void PairObjectInteraction(bool IsEnter, MovablePair movable)
    {
        if (IsEnter && !_matched)
        {
            _matched = (movable == _movablePair);
            if (_matched)
            {
                _matchSystem.NewMatchRecord(_matched);
                _feedback.ChangeMaterialWithMatch(_matched);
            }
            else if (!IsEnter && _matched)
            {
                _matched = !(movable == _movablePair);
                if (!_matched)
                {
                    _matchSystem.NewMatchRecord(_matched);
                    _feedback.ChangeMaterialWithMatch(_matched);
                }
            }
        }
    }
}