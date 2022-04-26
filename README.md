# RedKnight

＝＝＝＝＝＝＝ About development of the game ＝＝＝＝＝＝＝

This game, RedKnight, is entirely developed by myself.
It is a 2D action platformer game.
It is made using Unity Engine and C# is used.
Most of the assets used in the game are free assets from the internets. 
If you have any problem regarding the game, such as the sources of the assets, feel free to email to "wlwkwong@connect.ust.hk"

＝＝＝＝＝＝＝ About game control and difficulty ＝＝＝＝＝＝＝ 

Although all the essential game controls are introduced inside the game, I am also putting them here because the game is in Japanese.

A / D buttons:	Move left / right
Space button:	Jump
K button: Attack
L button: Proceed to the next line during dialogue
Left Control button: Dash

This game is intentionally made to be difficult and players are not expected to win against a boss in the first try.
However, every boss has idle time and you may find out how to beat them after a few tries.
The game length is around 20 minutes on average (it depends on how many times you die).

If you are not good at playing action games, you may take a look at the game-play videos.

＝＝＝＝＝＝＝ About Game-play videos ＝＝＝＝＝＝＝

I prepared 3 game-play videos.

The first video is a video that I played the game myself. 
It is around 7 minutes and I recommend watching this video if you want to understand the game flow.
URL: https://youtu.be/T1p4qR1GDfk

The second video is a video that I asked a friend of mine to test-play for me.
It is around 25 minutes. You may watch it if you want to watch a normal play.
URL:　https://youtu.be/Ww1CEVF3mes

The last video is a video that I asked another friend to test-play the game for me.
It is not the finalized version of the game and it is about 30 minutes long.
(I modified the game and corrected some bugs after receiving comments from my friend)
URL: https://youtu.be/jamzemzcxEg

＝＝＝＝＝＝＝ About C# Scripts ＝＝＝＝＝＝＝ 

All the scripts used in the game are attached in the Scripts folder. 
Comments are included in both English and Japanese in most of the scripts.
To make it easier to find scripts, I deleted the .meta files used by Unity Engine.
You may email me if you also want to read the .meta files.

＝＝＝＝＝＝＝ Appeal points of the game ＝＝＝＝＝＝＝ 

RedKnight is a completed game that all the essential functions of a game are implemented such as 
pausing the game, saving, deleting save files and volume adjustment button.

To improve game-play experience, I added several effects to the game.
For example, mirages are shown for a short period of time when the player is dashing.
Related scripts can be found at Scripts　ー＞　PlayerScript　ー＞　Mirage,　MiragePool

To make the game more exciting, the bosses will become more aggressive when their HP are below certain values.
For example, the boss "Slime King" of stage one will trigger "Pineapple trap" that works depending on the boss's stage.
If the boss is at stage one, the pineapples will drop randomly. 
For stage two and three, the probabilities of them dropping to the player's side increase. 
It prevents the player approaching the boss easily.
Related scripts can be found at Scripts　ー＞　Enemy　ー＞　SlimeKing　ー＞　PineappleSpawner.

＝＝＝＝＝＝＝ このゲームの制作について ＝＝＝＝＝＝＝

このゲーム、RedKnightは、すべて私ひとりで作ったものです。
RedKnightは横スクロールアクションゲームです。
ゲームエンジンはUnityで、プログラミング言語はC#を使っております。
ゲーム内で使用した背景、キャラクターやサウンドエフェクトなどはすべてネットにあるフリーアセットを調整したもの(大半)、もしくは自分で作ったものです。
アセットの出どころを知りたい場合、もしくはこのゲームについて何か質問がございましたら、ぜひ私にメールしてください。メールアドレスは、"wlwkwong@connect.ust.hk" です。

＝＝＝＝＝＝＝ ゲームの操作及び難易度などについて ＝＝＝＝＝＝＝

ゲームの具体的な操作方法はゲーム内に示しており、ゲーム内のヒントで必要な情報は足りるはずです。

ゲームの難易度は少し難しめに設定しており、簡単にはクリアできないようにしております。
しかし、決して無理やり難しくしているのではなく、各ボスにはちゃんと隙きがあります。
ボリュームについては、ゲームオーバーした回数に左右されますが、20分ほどでクリアできます。

プレイ動画を用意してありますので、アクションゲームが苦手な方、もしくはプレイするのが面倒な方は、プレイ動画をご覧になっていただけたら幸いです。

＝＝＝＝＝＝＝ プレイ動画について ＝＝＝＝＝＝＝

プレイ動画を３本用意してあります。　

"RedKnight プレイ動画＿本人"　は名前の通り、私自身がプレイした動画で、動画の長さは約７分です。
動画を一本だけご覧になる場合は、この動画をおすすめします。 
URL: https://youtu.be/T1p4qR1GDfk

"RedKnight プレイ動画＿友人A（修正前）"　は私が自分の友人（友人A）に頼んで遊んでいただいた動画で、まだ完成版ではありません。長さは約30分です。
（そのあと友人A氏から感想をいただいて、プレイヤーのエフェクトやボスの難易度を調整したり、バグを修正したりしました。）
URL: https://youtu.be/jamzemzcxEg

"RedKnight プレイ動画＿友人B"　は友人Bが遊んだ動画ですが、初見プレイのとき動画を撮り忘れていたようで、２回目のクリア動画になります。長さは約25分です。
URL:　https://youtu.be/Ww1CEVF3mes

＝＝＝＝＝＝＝ スクリプトについて　＝＝＝＝＝＝＝ 

RedKnightで使用したスクリプトは全部Scriptフォルダにあります。コードを読みやすくするために、適切なところにコメントを書いてあります。
（スクリプトを探しやすくするために、Unityエンジンで使われる .meta ファイルは削除しています。　.meta ファイルも読みたい場合は私にメールしてください）

＝＝＝＝＝＝＝ RedKnightのこだわりポイント　＝＝＝＝＝＝＝ 

RedKnightは完成したゲームです。ゲームで必要な機能、例えばセーブ機能、一時中断ボタン、音量調整ボタン、データ消去機能などは全部実装してあります。

ゲーム体験を高めるために、、様々なエフェクトを実装しました。
例えば、主人公キャラクターがダッシュするときは、単にキャラの速度を上げるのではなく、キャラの後ろに残像を残して、プレイヤーがダッシュしていることを強調しています。
（コードについてはScripts　ー＞　PlayerScript　ー＞　Mirage　及び　MiragePool　をご覧ください）

ゲーム性を高めるために、ボスたちにはそれぞれ３つの段階があります。ボスのHPが一定数まで下がったら、段階が上がるようにしており、ボスのパラメータや攻撃パターンが変化します。
例えば、ステージ１　"スライムの森"　のボス　"スライムキング"はパイナップル落下トラップを発動させます。
ボスが段階１のとき、パイナップルの落下位置は完全ランダムですが、ボスが段階２以上になると、パイナップルがボスの回りに落下する確率が上がり、プレイヤーが簡単にボスに近づけないようにしています。
コードについてはScripts　ー＞　Enemy　ー＞　SlimeKing　ー＞　PineappleSpawner　をご覧ください。
親クラス　Spawner　は　Scripts　ー＞　Trap　ー＞　Spawner　を　ご覧ください。