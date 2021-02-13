const VueLoaderPlugin = require('vue-loader/lib/plugin')
const TsconfigPathsPlugin = require('tsconfig-paths-webpack-plugin');
const path = require('path');

module.exports = {
    //モード値をproductionに設定すると最適化される
    //developmentに設定されるとソースマップ有効でJSファイルが出力される
    mode: "development",
    //エントリーポイント
    entry: "./src/main.ts",
    //出力先
    output: {
        //出力するディレクトリ
        path: `${__dirname}/dist`,
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
            },
            {
                //拡張子.vueの場合
                test: /\.vue$/,
                //Vueをコンパイルする
                use: 'vue-loader'
            }
        ]
    },
    resolve:{
        //import文で.ts書かなくていいようにする
        extensions:[".ts", ".js"],
        plugins:[
            //tsconfigのPathをwebpackで有効化
            new TsconfigPathsPlugin({
                configFile: './tsconfig.json'
            })
        ]
    },
    plugins: [
        new VueLoaderPlugin()
    ],
    devServer: {
        open: true,
        contentBase: path.resolve(__dirname, "dist"),
        port:8082
    }
}