using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyGuage : MonoBehaviour {

    public Image fill;
    public float fillDecayAmount = 0.1f;
    public float fillDecayTime = 1f;
    public Constants.GemColor color;

    public float shakeTime;
    public float shakeAmount = 0.01f;
    public Text comboText;
    public Text missedText;
    public int currCombo;
    public int numMissed;
    public List<GuageColorData> guageColors = new List<GuageColorData>();

    private float _timer;

    private float _fillMod;
    private bool _adjustFill;
    private bool _adjustFillColor;
    private Color _targetColor;
    private Constants.GemColor _currentColor;

    private Vector2 _basePosiiton;

    [System.Serializable]
    public class GuageColorData {
        public Constants.GemColor gemColor;
        public Color barColor = Color.white;
    }

    void Start() {
        _basePosiiton = transform.localPosition;
    }

    void Update() {

        if (_timer < fillDecayTime) {
            _timer += Time.deltaTime;
        }
        else {
            AdjustFill(-fillDecayAmount);
            _timer = 0f;
        }

        if (_adjustFill) {
            if (fill.fillAmount + _fillMod < fill.fillAmount || fill.fillAmount + _fillMod > fill.fillAmount) {
                fill.fillAmount = Mathf.Lerp(fill.fillAmount, fill.fillAmount + _fillMod, 1.0f * Time.deltaTime);
            }
            else {
                _adjustFill = false;
            }
        }

        if (fill.color != _targetColor) {
            fill.color = Color.Lerp(fill.color, _targetColor, Time.deltaTime);
        }

        if (shakeTime > 0) {
            Shake();
            shakeTime -= Time.deltaTime;
        }
        if (Vector2.Distance(transform.localPosition, _basePosiiton) > 1.5f) {
            transform.localPosition = _basePosiiton;
        }
    }

    public void Shake() {
        Vector2 shakes = Random.insideUnitCircle * shakeAmount;
        transform.position += new Vector3(shakes.x, shakes.y, 0f);
    }

    public void AdjustFill(float amount) {

        if (amount < 0 && fill.fillAmount <= 0) {
            return;
        }

        if (amount > 0 && fill.fillAmount >= 1) {
            return;
        }

        if (fill.fillAmount <= 0f) {
            _fillMod = 0f;
        }

        if (fill.fillAmount >= 1) {
            _fillMod = 0f;
        }

        if (amount > 0) {
            amount += (currCombo *.01f);
            shakeTime += 0.2f;
        }

        _fillMod = amount;

        _adjustFill = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Fragment") {

            GemFragment fragScript = other.GetComponent<GemFragment>();

            if (fragScript.fragmentColor == color || color == Constants.GemColor.All) {
                AdjustFill(.01f);

                if (fragScript.fragmentColor == _currentColor) {
                    currCombo++;
                }
                else {
                    currCombo = 0;

                }
                comboText.text = "Combo: X" + currCombo;

                for (int i = 0; i < guageColors.Count; i++) {
                    if (fragScript.fragmentColor == guageColors[i].gemColor) {
                        _targetColor = guageColors[i].barColor;
                        _currentColor = fragScript.fragmentColor;
                    }
                }

                other.GetComponent<GemFragment>().Die();
            }
        }
    }

}