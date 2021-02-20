Shader "MaskedObject"
{
  Properties
  {
    _Mask ("Mask", Int) = 1
  }
  SubShader
  {
    // タグ付け
    Tags 
    { 
      // "RenderType(レンダータイプ)"を"Opaque(不透明)"に指定
      "RenderType" = "Opaque" 
      // "Queue(描画順)"を"Geometry(デフォルト)-2"に指定
      // つまり先ほどの Wall オブジェクトより先駆けて描画する
      "Queue" = "Geometry-2"
    }
    CGINCLUDE
    // "UnityCG.cginc"をインクルード
    #include "UnityCG.cginc"

    // v2f構造体の定義
    // v2fという名前は慣例で Vertex To Fragmentの略
    // 頂点シェーダからフラグメントシェーダに複数の値を渡す時に利用する
    struct v2f
    {
      // float4 SV_POSITION  MVP 変換後の座標
      float4 vertex : SV_POSITION;
      // VRのシングルパスステレオレンダリングのマクロ
      UNITY_VERTEX_OUTPUT_STEREO
    };
    // 頂点シェーダ
    v2f vert(appdata_base v)
    {
      // インスタンス ID がシェーダー関数にアクセス可能になる
      UNITY_SETUP_INSTANCE_ID(v);
      // 返り値として利用するv2f構造体の変数を作成
      v2f o;
      // 同次座標において、オブジェクト空間からカメラのクリップ空間へ点を変換する
      o.vertex = UnityObjectToClipPos(v.vertex);
      // VR向けの頂点シェーダーへの変換を行う
      UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
      // 構造体を返す
      return o;
    }
    // フラグメントシェーダ
    fixed4 frag (v2f i) : SV_Target
    {
      // 描画するカラーを返却する
      // 必ず 0 を返却する
      // ただし ColorMask 0 が設定されているため、カラーチャネルは書き込まれない
      return 0;
    }
    ENDCG

    Pass
    {
      // カラーチャネルの書き込みを設定
      // ColorMask 0 ですべてのカラーチャネルのレンダリングを無効化
      ColorMask 0
      // ZWrite:デプスバッファに書き込みするか制御
      // 透明なオブジェクトを描画する場合、Off
      ZWrite Off
      // Stencil:ステンシルバッファはピクセルマスクごとにピクセルを保存や廃棄することを目的とする
      // ステンシルバッファは、通常、1 ピクセルあたり 8 ビットの整数である
      Stencil 
      {
        // バッファに_Maskの 1 の整数が書き込まれる
        Ref [_Mask]
        // Comp:関数はバッファの現在の内容と基準値の比較に使用される
        // Always:常にステンシルテストをパスさせる
        Comp Always
        // Pass:ステンシルテスト（及びデプステスト）をパスした場合、バッファの内容をどうするか決める
        // Replace:リファレンス値をバッファに書き込む
        Pass Replace
      }

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      ENDCG
    }
  }
}