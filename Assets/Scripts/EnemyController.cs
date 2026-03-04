using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;


public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("移動経路の情報")]
    private PathData pathData;

    [SerializeField, Header("移動速度")]
    private float moveSpeed;

    private Vector3[] paths;

    private Animator anim;       // Animator コンポーネントの取得用

    void Start()
    {
        // Animator コンポーネントを取得して anim 変数に代入
        TryGetComponent(out anim);

        // 移動する地点を取得
        paths = pathData.pathTranArray.Select(x => x.position).ToArray();

        // 各地点に向けて移動
        transform.DOPath(paths, 1000 / moveSpeed).SetEase(Ease.Linear).OnWaypointChange(ChangeAnimeDirection);  //  <=  ３つ目のメソッドを追加します。


    }


    /// <summary>
    /// 敵の進行方向を取得して、移動アニメと同期
    /// </summary>
    private void ChangeAnimeDirection(int index)
    {　　　　　　//　<=　☆①　引数を追加します

        Debug.Log(index);                   //　<=　☆②　ここから if 文全文を追加します

        // 次の移動先の地点がない場合には、ここで処理を終了する
        if (index >= paths.Length)
        {
            return;
        }　　　　　　　　　　　　　　　　　　　　　　　　　　 //　<=　☆②　ここまで

        if (transform.position.x > paths[index].x)
        {　　　　　//　<=　☆③　条件式の右辺を変更します。演算子の方向に注意してください
            anim.SetFloat("Y", 0f);
            anim.SetFloat("X", -1.0f);

            Debug.Log("左方向");

        }
        else if (transform.position.y < paths[index].y)
        {　 //　<=　☆④　条件式の右辺を変更します。演算子の方向に注意してください
            anim.SetFloat("X", 0f);
            anim.SetFloat("Y", 1.0f);

            Debug.Log("上左向");

        }
        else if (transform.position.y > paths[index].y)
        {　 //　<=　☆⑤　条件式の右辺を変更します。演算子の方向に注意してください
            anim.SetFloat("X", 0f);
            anim.SetFloat("Y", -1.0f);

            Debug.Log("下方向");

        }
        else
        {
            anim.SetFloat("Y", 0f);
            anim.SetFloat("X", 1.0f);

            Debug.Log("右方向");
        }
    }
}