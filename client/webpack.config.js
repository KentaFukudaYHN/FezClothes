module.exports = {
    //モード値をproductionに設定すると最適化される
    //developmentに設定されるとソースマップ有効でJSファイルが出力される
    mode: "development",
    //エントリーポイント
    entry: "./src/main.ts",
    //出力先
    output: {
        //出力するディレクトリ
        path: "${__dirname}/dist",
        //出力するファイル名
        filename: "main.js"
    },
    module:{
        rules:[
            {
                //拡張子.tsの場合
                test: /\.ts$/,
                //TypeScriptをコンパイルする
                use: "ts-loader"
            }
        ]
    },
    resolve:{
        //import文で.ts書かなくていいようにする
        extensions:[".ts"],
        alias:{
            vue: "vue/dist/vue.js"
        }
    }

}