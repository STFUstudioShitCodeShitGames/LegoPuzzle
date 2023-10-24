using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class ShmerPer : MonoBehaviour
{
    [SerializeField] private Chipinki _chipinki;

    [SerializeField] private GameObject _hulp;
    [SerializeField] private GameObject _unhulp;

    private Chipinki.Shuraba[] _shurabas;
    private Dictionary<int, Chastichka> _pobedna = new();

    private Dictionary<Vector2Int, Chastichka> _inActualki = new();

    private Chipinki.Shuraba _tec;
    private int _tecNum;

    private Coroutine _shuraba;

    private void Start()
    {
        Reculas();
        
        Cjomba(true);
    }

    public void Reculas()
    {
        _shurabas = new Chipinki.Shuraba[_chipinki.Shurabinki.Count];
        _chipinki.Shurabinki.CopyTo(_shurabas, 0);
        ShamBaram(_shurabas);

        _tec = _shurabas[_tecNum];

        Humas();
    }

    private void Cjomba(bool glumsh)
    {
        _hulp.SetActive(glumsh);
        _unhulp.SetActive(!glumsh);
    }

    private void Humas()
    {
        var hkj = _tec.Chastichki;
        var verc = transform.position;
        var oneVec = 0;
        var secVec = 0;
        for (var bibo = 0; bibo < hkj.Count; bibo++)
        {
            var stor = hkj[bibo].rect.width / 100f;
            var vis = hkj[bibo].rect.height / 100f;

            if (bibo != 0 && bibo % 2 == 0)
            {
                oneVec = 0;
                secVec++;
                verc.x = transform.position.x;
                verc.y -= vis;
            }

            var chast = new GameObject("asddd", typeof(SpriteRenderer), typeof(Chastichka));
            var asdas = chast.GetComponent<Chastichka>();
            asdas.Hus = bibo;
            chast.transform.parent = transform;
            chast.transform.position = verc;
            asdas.AwukePos = verc;
            asdas.Sus.sprite = hkj[bibo];
            asdas.Sus.sortingLayerName = "GamePlay";

            _pobedna.Add(bibo, asdas);
            _inActualki.Add(new Vector2Int(oneVec, secVec), asdas);

            asdas.Voshel += OnVoshel;
            
            verc.x += stor;
            oneVec++;
        }

        _shuraba = StartCoroutine(ChikTak());
    }

    private IEnumerator ChikTak()
    {
        yield return new WaitForSecondsRealtime(1f);
        Shubka();
        
        StopCoroutine(_shuraba);
        _shuraba = null;
    }

    private void Shubka()
    {
        _pobedna[0].Sus.enabled = false;
        
        var kuba = _inActualki.Keys.Count;
        while (kuba > 1)
        {
            var uruas = new Random()
                .Next(kuba--);

            var fif = _inActualki.FirstOrDefault(x => x.Value == _pobedna[kuba]).Key;
            var fuf = _inActualki.FirstOrDefault(x => x.Value == _pobedna[uruas]).Key;

            (_inActualki[fif], _inActualki[fuf]) = (_inActualki[fuf], _inActualki[fif]);
            
            (_pobedna[kuba].transform.position, _pobedna[uruas].transform.position) = (
                _pobedna[uruas].transform.position, _pobedna[kuba].transform.position);
        }
    }

    private void OnVoshel(Chastichka chastichka)
    {
        var fif = _inActualki.FirstOrDefault(x => x.Value == chastichka).Key;

            for (var glii = 0; glii < 4; glii++)
            {
                var fin = fif;

                switch (glii)
                {
                    case 0:
                        fin += Vector2Int.up;
                        break;
                    case 1:
                        fin += Vector2Int.right;
                        break;
                    case 2:
                        fin += Vector2Int.down;
                        break;
                    case 3:
                        fin += Vector2Int.left;
                        break;
                }

                if (!_inActualki.ContainsKey(fin))
                    continue;

                if (!_inActualki[fin].Zamen)
                    continue;

                (_inActualki[fif], _inActualki[fin]) = (_inActualki[fin], _inActualki[fif]);

                (_inActualki[fif].transform.position, _inActualki[fin].transform.position) = (
                    _inActualki[fin].transform.position, _inActualki[fif].transform.position);

                break;
            }
            
        Proverochka();
    }

    private void Proverochka()
    {
        var kibaraj = 0;
        foreach (var chastichka in _inActualki.Values)
        {
            if (chastichka.Hus != kibaraj)
                return;

            kibaraj++;
        }

        _pobedna[0].Sus.enabled = true;

        UsePobeda();
    }

    private void UsePobeda()
    {
        _chingik = StartCoroutine(Chingik());
    }

    private Coroutine _chingik;

    private IEnumerator Chingik()
    {
        yield return new WaitForSeconds(1f);

        _tecNum++;
        if (_tecNum >= _chipinki.Shurabinki.Count)
            _tecNum = 0;

        foreach (var value in _inActualki.Values)
        {
            value.Voshel -= OnVoshel;
            Destroy(value.gameObject);
        }
        
        _pobedna.Clear();
        _inActualki.Clear();
        
        Reculas();
        StopCoroutine(_chingik);
    }

    private List<Vector2> _ufse = new ();

    public void Trudna()
    {
        if (_shuraba != null)
            return;
        
        Cjomba(false);

        _ufse.Clear();
        _ufse = new List<Vector2>();

        _pobedna[0].Sus.enabled = true;
        
        foreach (var chi in _inActualki.Values)
        {
            _ufse.Add(chi.transform.position);
            chi.transform.position = chi.AwukePos;
        }
    }

    public void Smogu()
    {
        Cjomba(true);

        var ubka = 0;

        _pobedna[0].Sus.enabled = false;

        foreach (var actualkiValue in _inActualki.Values)
        {
            actualkiValue.transform.position = _ufse[ubka];
            ubka++;
        }
    }
    
    private void ShamBaram<T>(T[] kuka)
    {
        var Jimba = kuka.Length;
        while (Jimba > 1)
        {
            var uruas = new Random()
                .Next(Jimba--);
            (kuka[Jimba], kuka[uruas]) = (kuka[uruas], kuka[Jimba]);
        }
    }
}