ë
wD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Infrastructure\Services\SmsNotificationService.cs
	namespace 	
Producao
 
. 
Infrastructure !
.! "
Services" *
;* +
public		 
class		 "
SmsNotificationService		 #
:		$ % 
INotificationService		& :
{

 
private 
readonly 
ILogger 
< "
SmsNotificationService 3
>3 4
_logger5 <
;< =
public 
"
SmsNotificationService !
(! "
ILogger" )
<) *"
SmsNotificationService* @
>@ A
loggerB H
)H I
{ 
_logger 
= 
logger 
; 
} 
public 

async 
Task (
NotifyOrderStatusChangeAsync 2
(2 3
Guid3 7
orderId8 ?
,? @
stringA G
previousStatusH V
,V W
stringX ^
	newStatus_ h
,h i
Guidj n
?n o

customerIdp z
)z {
{ 
if 

( 

customerId 
== 
null 
) 
{ 	
_logger 
. 
LogInformation "
(" #
$str# ]
,] ^
orderId_ f
)f g
;g h
return 
; 
} 	
_logger 
. 
LogInformation 
( 
$str	 Å
,
Å Ç

customerId 
, 
orderId 
,  
previousStatus! /
,/ 0
	newStatus1 :
): ;
;; <
await 
Task 
. 
Delay 
( 
$num 
) 
; 
_logger!! 
.!! 
LogInformation!! 
(!! 
$str"" N
,""N O

customerId## 
,## 
orderId## 
)##  
;##  !
}$$ 
public&& 

async&& 
Task&& !
NotifyOrderReadyAsync&& +
(&&+ ,
Guid&&, 0
orderId&&1 8
,&&8 9
Guid&&: >
?&&> ?

customerId&&@ J
)&&J K
{'' 
if(( 

((( 

customerId(( 
==(( 
null(( 
)(( 
{)) 	
_logger** 
.** 
LogInformation** "
(**" #
$str**# ]
,**] ^
orderId**_ f
)**f g
;**g h
return++ 
;++ 
},, 	
_logger.. 
... 
LogInformation.. 
(.. 
$str// e
,//e f

customerId00 
,00 
orderId00 
)00  
;00  !
await33 
Task33 
.33 
Delay33 
(33 
$num33 
)33 
;33 
_logger55 
.55 
LogInformation55 
(55 
$str66 _
,66_ `

customerId77 
,77 
orderId77 
)77  
;77  !
}88 
}99 ã"
sD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Infrastructure\Services\OrderServiceClient.cs
	namespace 	
Producao
 
. 
Infrastructure !
.! "
Services" *
;* +
public 
class 
OrderServiceClient 
:  !
IOrderServiceClient" 5
{ 
private 
readonly 

HttpClient 
_httpClient  +
;+ ,
private 
readonly 
ILogger 
< 
OrderServiceClient /
>/ 0
_logger1 8
;8 9
public 

OrderServiceClient 
( 

HttpClient 

httpClient 
, 
IConfiguration 
configuration $
,$ %
ILogger 
< 
OrderServiceClient "
>" #
logger$ *
)* +
{ 
_httpClient 
= 

httpClient  
;  !
_logger 
= 
logger 
; 
var 
orderServiceBaseUrl 
=  !
configuration" /
[/ 0
$str0 H
]H I
?? 
throw 
new %
InvalidOperationException 2
(2 3
$str3 [
)[ \
;\ ]
_httpClient 
. 
BaseAddress 
=  !
new" %
Uri& )
() *
orderServiceBaseUrl* =
)= >
;> ?
_httpClient 
. 
Timeout 
= 
TimeSpan &
.& '
FromSeconds' 2
(2 3
$num3 5
)5 6
;6 7
} 
public 

async 
Task 
< 
bool 
> "
UpdateOrderStatusAsync 2
(2 3
Guid3 7
orderId8 ?
,? @
stringA G
statusH N
,N O
CancellationTokenP a
cancellationTokenb s
=t u
defaultv }
)} ~
{   
try!! 
{"" 	
_logger## 
.## 
LogInformation## "
(##" #
$str### w
,##w x
orderId	##y Ä
,
##Ä Å
status
##Ç à
)
##à â
;
##â ä
var%% 
requestBody%% 
=%% 
new%% !
{%%" #
Status%%$ *
=%%+ ,
status%%- 3
}%%4 5
;%%5 6
var&& 
response&& 
=&& 
await&&  
_httpClient&&! ,
.&&, -
PutAsJsonAsync&&- ;
(&&; <
$"'' 
$str'' !
{''! "
orderId''" )
}'') *
$str''* 1
"''1 2
,''2 3
requestBody(( 
,(( 
cancellationToken)) !
)))! "
;))" #
if++ 
(++ 
!++ 
response++ 
.++ 
IsSuccessStatusCode++ -
)++- .
{,, 
var-- 
errorContent--  
=--! "
await--# (
response--) 1
.--1 2
Content--2 9
.--9 :
ReadAsStringAsync--: K
(--K L
cancellationToken--L ]
)--] ^
;--^ _
_logger.. 
... 
LogError..  
(..  !
$str..! n
,..n o
response// 
.// 

StatusCode// '
,//' (
errorContent//) 5
)//5 6
;//6 7
return11 
false11 
;11 
}22 
_logger44 
.44 
LogInformation44 "
(44" #
$str44# y
,44y z
orderId55 
,55 
status55 
)55  
;55  !
return77 
true77 
;77 
}88 	
catch99 
(99 
	Exception99 
ex99 
)99 
{:: 	
_logger;; 
.;; 
LogError;; 
(;; 
ex;; 
,;;  
$str;;! W
);;W X
;;;X Y
return<< 
false<< 
;<< 
}== 	
}>> 
}?? ‹
yD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Infrastructure\Services\EmailNotificationService.cs
	namespace 	
Producao
 
. 
Infrastructure !
.! "
Services" *
;* +
public		 
class		 $
EmailNotificationService		 %
:		& ' 
INotificationService		( <
{

 
private 
readonly 
ILogger 
< $
EmailNotificationService 5
>5 6
_logger7 >
;> ?
public 
$
EmailNotificationService #
(# $
ILogger$ +
<+ ,$
EmailNotificationService, D
>D E
loggerF L
)L M
{ 
_logger 
= 
logger 
; 
} 
public 

async 
Task (
NotifyOrderStatusChangeAsync 2
(2 3
Guid3 7
orderId8 ?
,? @
stringA G
previousStatusH V
,V W
stringX ^
	newStatus_ h
,h i
Guidj n
?n o

customerIdp z
)z {
{ 
if 

( 

customerId 
== 
null 
) 
{ 	
_logger 
. 
LogInformation "
(" #
$str# ]
,] ^
orderId_ f
)f g
;g h
return 
; 
} 	
_logger 
. 
LogInformation 
( 
$str {
,{ |

customerId 
, 
orderId 
,  
previousStatus! /
,/ 0
	newStatus1 :
): ;
;; <
string 
emailSubject 
= 
$"  
$str  ;
{; <
orderId< C
}C D
"D E
;E F
await   
Task   
.   
Delay   
(   
$num   
)   
;   
_logger"" 
."" 
LogInformation"" 
("" 
$str## d
,##d e

customerId$$ 
,$$ 
orderId$$ 
,$$  
emailSubject$$! -
)$$- .
;$$. /
}%% 
public'' 

async'' 
Task'' !
NotifyOrderReadyAsync'' +
(''+ ,
Guid'', 0
orderId''1 8
,''8 9
Guid'': >
?''> ?

customerId''@ J
)''J K
{(( 
if)) 

()) 

customerId)) 
==)) 
null)) 
))) 
{** 	
_logger++ 
.++ 
LogInformation++ "
(++" #
$str++# ]
,++] ^
orderId++_ f
)++f g
;++g h
return,, 
;,, 
}-- 	
_logger// 
.// 
LogInformation// 
(// 
$str00 _
,00_ `

customerId11 
,11 
orderId11 
)11  
;11  !
string33 
emailSubject33 
=33 
$"33  
$str33  ,
{33, -
orderId33- 4
}334 5
$str335 P
"33P Q
;33Q R
await55 
Task55 
.55 
Delay55 
(55 
$num55 
)55 
;55 
_logger77 
.77 
LogInformation77 
(77 
$str88 u
,88u v

customerId99 
,99 
orderId99 
,99  
emailSubject99! -
)99- .
;99. /
}:: 
};; œÑ
~D:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Infrastructure\Repositories\ProductionOrderRepository.cs
	namespace 	
Producao
 
. 
Infrastructure !
.! "
Repositories" .
;. /
public 
class %
ProductionOrderRepository &
:' (&
IProductionOrderRepository) C
{ 
private 
readonly 
IMongoCollection %
<% &
ProductionOrder& 5
>5 6
_collection7 B
;B C
public 
%
ProductionOrderRepository $
($ %
ProductionDbContext% 8
context9 @
)@ A
{ 
_collection 
= 
context 
. 
ProductionOrders .
;. /
} 
public 

async 
Task 
< 
ProductionOrder %
?% &
>& '
GetByIdAsync( 4
(4 5
Guid5 9
id: <
,< =
CancellationToken> O
cancellationTokenP a
=b c
defaultd k
)k l
{ 
var 
filter 
= 
Builders 
< 
ProductionOrder -
>- .
.. /
Filter/ 5
.5 6
Eq6 8
(8 9
p9 :
=>; =
p> ?
.? @
Id@ B
,B C
idD F
)F G
;G H
var 
options 
= 
new 
FindOptions %
<% &
ProductionOrder& 5
,5 6
ProductionOrder7 F
>F G
{H I
LimitJ O
=P Q
$numR S
}T U
;U V
using 
var 
cursor 
= 
await  
_collection! ,
., -
	FindAsync- 6
(6 7
filter7 =
,= >
options? F
,F G
cancellationTokenH Y
)Y Z
;Z [
return 
await )
FirstOrDefaultFromCursorAsync 2
(2 3
cursor3 9
,9 :
cancellationToken; L
)L M
;M N
} 
public 

async 
Task 
< 
IEnumerable !
<! "
ProductionOrder" 1
>1 2
>2 3
GetAllAsync4 ?
(? @
int@ C

pageNumberD N
=O P
$numQ R
,R S
intT W
pageSizeX `
=a b
$numc e
,e f
CancellationTokeng x
cancellationToken	y ä
=
ã å
default
ç î
)
î ï
{ 
var   
filter   
=   
Builders   
<   
ProductionOrder   -
>  - .
.  . /
Filter  / 5
.  5 6
Empty  6 ;
;  ; <
var"" 
options"" 
="" 
new"" 
FindOptions"" %
<""% &
ProductionOrder""& 5
,""5 6
ProductionOrder""7 F
>""F G
{## 	
Sort$$ 
=$$ 
Builders$$ 
<$$ 
ProductionOrder$$ +
>$$+ ,
.$$, -
Sort$$- 1
.$$1 2

Descending$$2 <
($$< =
p$$= >
=>$$? A
p$$B C
.$$C D
	CreatedAt$$D M
)$$M N
,$$N O
Skip%% 
=%% 
(%% 

pageNumber%% 
-%%  
$num%%! "
)%%" #
*%%$ %
pageSize%%& .
,%%. /
Limit&& 
=&& 
pageSize&& 
}'' 	
;''	 

using)) 
var)) 
cursor)) 
=)) 
await))  
_collection))! ,
.)), -
	FindAsync))- 6
())6 7
filter))7 =
,))= >
options))? F
,))F G
cancellationToken))H Y
)))Y Z
;))Z [
return** 
await** !
ToListFromCursorAsync** *
(*** +
cursor**+ 1
,**1 2
cancellationToken**3 D
)**D E
;**E F
}++ 
public-- 

async-- 
Task-- 
<-- 
ProductionOrder-- %
>--% &
CreateAsync--' 2
(--2 3
ProductionOrder--3 B
entity--C I
,--I J
CancellationToken--K \
cancellationToken--] n
=--o p
default--q x
)--x y
{.. 
await// 
_collection// 
.// 
InsertOneAsync// (
(//( )
entity//) /
,/// 0
cancellationToken//1 B
://B C
cancellationToken//D U
)//U V
;//V W
return00 
entity00 
;00 
}11 
public33 

async33 
Task33 
UpdateAsync33 !
(33! "
ProductionOrder33" 1
entity332 8
,338 9
CancellationToken33: K
cancellationToken33L ]
=33^ _
default33` g
)33g h
{44 
await55 
_collection55 
.55 
ReplaceOneAsync55 )
(55) *
p66 
=>66 
p66 
.66 
Id66 
==66 
entity66 
.66  
Id66  "
,66" #
entity77 
,77 
cancellationToken88 
:88 
cancellationToken88 0
)880 1
;881 2
}99 
public;; 

async;; 
Task;; 
DeleteAsync;; !
(;;! "
ProductionOrder;;" 1
entity;;2 8
,;;8 9
CancellationToken;;: K
cancellationToken;;L ]
=;;^ _
default;;` g
);;g h
{<< 
await== 
_collection== 
.== 
DeleteOneAsync== (
(==( )
p==) *
=>==+ -
p==. /
.==/ 0
Id==0 2
====3 5
entity==6 <
.==< =
Id=== ?
,==? @
cancellationToken==A R
)==R S
;==S T
}>> 
public@@ 

async@@ 
Task@@ 
<@@ 
ProductionOrder@@ %
?@@% &
>@@& '
GetByOrderIdAsync@@( 9
(@@9 :
Guid@@: >
orderId@@? F
,@@F G
CancellationToken@@H Y
cancellationToken@@Z k
=@@l m
default@@n u
)@@u v
{AA 
varBB 
filterBB 
=BB 
BuildersBB 
<BB 
ProductionOrderBB -
>BB- .
.BB. /
FilterBB/ 5
.BB5 6
EqBB6 8
(BB8 9
pBB9 :
=>BB; =
pBB> ?
.BB? @
OrderIdBB@ G
,BBG H
orderIdBBI P
)BBP Q
;BBQ R
varDD 
optionsDD 
=DD 
newDD 
FindOptionsDD %
<DD% &
ProductionOrderDD& 5
,DD5 6
ProductionOrderDD7 F
>DDF G
{DDH I
LimitDDJ O
=DDP Q
$numDDR S
}DDT U
;DDU V
usingEE 
varEE 
cursorEE 
=EE 
awaitEE  
_collectionEE! ,
.EE, -
	FindAsyncEE- 6
(EE6 7
filterEE7 =
,EE= >
optionsEE? F
,EEF G
cancellationTokenEEH Y
)EEY Z
;EEZ [
returnFF 
awaitFF )
FirstOrDefaultFromCursorAsyncFF 2
(FF2 3
cursorFF3 9
,FF9 :
cancellationTokenFF; L
)FFL M
;FFM N
}GG 
privateII 
staticII 
asyncII 
TaskII 
<II 
ProductionOrderII -
?II- .
>II. /)
FirstOrDefaultFromCursorAsyncII0 M
(IIM N
IAsyncCursorIIN Z
<IIZ [
ProductionOrderII[ j
>IIj k
cursorIIl r
,IIr s
CancellationToken	IIt Ö
cancellationToken
IIÜ ó
)
IIó ò
{JJ 
ifKK 

(KK 
awaitKK 
cursorKK 
.KK 
MoveNextAsyncKK &
(KK& '
cancellationTokenKK' 8
)KK8 9
)KK9 :
{LL 	
returnMM 
cursorMM 
.MM 
CurrentMM !
.MM! "
FirstOrDefaultMM" 0
(MM0 1
)MM1 2
;MM2 3
}NN 	
returnPP 
nullPP 
;PP 
}QQ 
privateSS 
staticSS 
asyncSS 
TaskSS 
<SS 
ListSS "
<SS" #
ProductionOrderSS# 2
>SS2 3
>SS3 4!
ToListFromCursorAsyncSS5 J
(SSJ K
IAsyncCursorSSK W
<SSW X
ProductionOrderSSX g
>SSg h
cursorSSi o
,SSo p
CancellationToken	SSq Ç
cancellationToken
SSÉ î
)
SSî ï
{TT 
varUU 
resultUU 
=UU 
newUU 
ListUU 
<UU 
ProductionOrderUU -
>UU- .
(UU. /
)UU/ 0
;UU0 1
whileWW 
(WW 
awaitWW 
cursorWW 
.WW 
MoveNextAsyncWW )
(WW) *
cancellationTokenWW* ;
)WW; <
)WW< =
{XX 	
resultYY 
.YY 
AddRangeYY 
(YY 
cursorYY "
.YY" #
CurrentYY# *
)YY* +
;YY+ ,
}ZZ 	
return\\ 
result\\ 
;\\ 
}]] 
public__ 

async__ 
Task__ 
<__ 
IEnumerable__ !
<__! "
ProductionOrder__" 1
>__1 2
>__2 3
GetByStatusAsync__4 D
(__D E
ProductionStatus__E U
status__V \
,__\ ]
int__^ a

pageNumber__b l
=__m n
$num__o p
,__p q
int__r u
pageSize__v ~
=	__ Ä
$num
__Å É
,
__É Ñ
CancellationToken
__Ö ñ
cancellationToken
__ó ®
=
__© ™
default
__´ ≤
)
__≤ ≥
{`` 
varaa 
filteraa 
=aa 
Buildersaa 
<aa 
ProductionOrderaa -
>aa- .
.aa. /
Filteraa/ 5
.aa5 6
Eqaa6 8
(aa8 9
paa9 :
=>aa; =
paa> ?
.aa? @
Statusaa@ F
,aaF G
statusaaH N
)aaN O
;aaO P
varcc 
optionscc 
=cc 
newcc 
FindOptionscc %
<cc% &
ProductionOrdercc& 5
,cc5 6
ProductionOrdercc7 F
>ccF G
{dd 	
Sortee 
=ee 
Buildersee 
<ee 
ProductionOrderee +
>ee+ ,
.ee, -
Sortee- 1
.ee1 2

Descendingee2 <
(ee< =
pee= >
=>ee? A
peeB C
.eeC D
	CreatedAteeD M
)eeM N
,eeN O
Skipff 
=ff 
(ff 

pageNumberff 
-ff  
$numff! "
)ff" #
*ff$ %
pageSizeff& .
,ff. /
Limitgg 
=gg 
pageSizegg 
}hh 	
;hh	 

usingjj 
varjj 
cursorjj 
=jj 
awaitjj  
_collectionjj! ,
.jj, -
	FindAsyncjj- 6
(jj6 7
filterjj7 =
,jj= >
optionsjj? F
,jjF G
cancellationTokenjjH Y
)jjY Z
;jjZ [
returnkk 
awaitkk !
ToListFromCursorAsynckk *
(kk* +
cursorkk+ 1
,kk1 2
cancellationTokenkk3 D
)kkD E
;kkE F
}ll 
publicnn 

asyncnn 
Tasknn 
<nn 
IEnumerablenn !
<nn! "
ProductionOrdernn" 1
>nn1 2
>nn2 3&
GetOrdersInProductionAsyncnn4 N
(nnN O
intnnO R

pageNumbernnS ]
=nn^ _
$numnn` a
,nna b
intnnc f
pageSizenng o
=nnp q
$numnnr t
,nnt u
CancellationToken	nnv á
cancellationToken
nnà ô
=
nnö õ
default
nnú £
)
nn£ §
{oo 
varpp 
statusFilterpp 
=pp 
Builderspp #
<pp# $
ProductionOrderpp$ 3
>pp3 4
.pp4 5
Filterpp5 ;
.pp; <
Inpp< >
(pp> ?
pqq 
=>qq 
pqq 
.qq 
Statusqq 
,qq 
newrr 
[rr 
]rr 
{rr 
ProductionStatusrr $
.rr$ %
Receivedrr% -
,rr- .
ProductionStatusrr/ ?
.rr? @
InPreparationrr@ M
}rrN O
)rrO P
;rrP Q
vartt 
optionstt 
=tt 
newtt 
FindOptionstt %
<tt% &
ProductionOrdertt& 5
,tt5 6
ProductionOrdertt7 F
>ttF G
{uu 	
Sortvv 
=vv 
Buildersvv 
<vv 
ProductionOrdervv +
>vv+ ,
.vv, -
Sortvv- 1
.vv1 2

Descendingvv2 <
(vv< =
pvv= >
=>vv? A
pvvB C
.vvC D
	CreatedAtvvD M
)vvM N
,vvN O
Skipww 
=ww 
(ww 

pageNumberww 
-ww  
$numww! "
)ww" #
*ww$ %
pageSizeww& .
,ww. /
Limitxx 
=xx 
pageSizexx 
}yy 	
;yy	 

using{{ 
var{{ 
cursor{{ 
={{ 
await{{  
_collection{{! ,
.{{, -
	FindAsync{{- 6
({{6 7
statusFilter{{7 C
,{{C D
options{{E L
,{{L M
cancellationToken{{N _
){{_ `
;{{` a
return|| 
await|| !
ToListFromCursorAsync|| *
(||* +
cursor||+ 1
,||1 2
cancellationToken||3 D
)||D E
;||E F
}}} 
public 

async 
Task 
< 
IEnumerable !
<! "
ProductionOrder" 1
>1 2
>2 3
GetReadyOrdersAsync4 G
(G H
intH K

pageNumberL V
=W X
$numY Z
,Z [
int\ _
pageSize` h
=i j
$numk m
,m n
CancellationToken	o Ä
cancellationToken
Å í
=
ì î
default
ï ú
)
ú ù
{
ÄÄ 
var
ÅÅ 
filter
ÅÅ 
=
ÅÅ 
Builders
ÅÅ 
<
ÅÅ 
ProductionOrder
ÅÅ -
>
ÅÅ- .
.
ÅÅ. /
Filter
ÅÅ/ 5
.
ÅÅ5 6
Eq
ÅÅ6 8
(
ÅÅ8 9
p
ÅÅ9 :
=>
ÅÅ; =
p
ÅÅ> ?
.
ÅÅ? @
Status
ÅÅ@ F
,
ÅÅF G
ProductionStatus
ÅÅH X
.
ÅÅX Y
Ready
ÅÅY ^
)
ÅÅ^ _
;
ÅÅ_ `
var
ÉÉ 
options
ÉÉ 
=
ÉÉ 
new
ÉÉ 
FindOptions
ÉÉ %
<
ÉÉ% &
ProductionOrder
ÉÉ& 5
,
ÉÉ5 6
ProductionOrder
ÉÉ7 F
>
ÉÉF G
{
ÑÑ 	
Sort
ÖÖ 
=
ÖÖ 
Builders
ÖÖ 
<
ÖÖ 
ProductionOrder
ÖÖ +
>
ÖÖ+ ,
.
ÖÖ, -
Sort
ÖÖ- 1
.
ÖÖ1 2

Descending
ÖÖ2 <
(
ÖÖ< =
p
ÖÖ= >
=>
ÖÖ? A
p
ÖÖB C
.
ÖÖC D
ReadyAt
ÖÖD K
)
ÖÖK L
,
ÖÖL M
Skip
ÜÜ 
=
ÜÜ 
(
ÜÜ 

pageNumber
ÜÜ 
-
ÜÜ  
$num
ÜÜ! "
)
ÜÜ" #
*
ÜÜ$ %
pageSize
ÜÜ& .
,
ÜÜ. /
Limit
áá 
=
áá 
pageSize
áá 
}
àà 	
;
àà	 

using
ää 
var
ää 
cursor
ää 
=
ää 
await
ää  
_collection
ää! ,
.
ää, -
	FindAsync
ää- 6
(
ää6 7
filter
ää7 =
,
ää= >
options
ää? F
,
ääF G
cancellationToken
ääH Y
)
ääY Z
;
ääZ [
return
ãã 
await
ãã #
ToListFromCursorAsync
ãã *
(
ãã* +
cursor
ãã+ 1
,
ãã1 2
cancellationToken
ãã3 D
)
ããD E
;
ããE F
}
åå 
}çç â
pD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Infrastructure\Data\ProductionDbContext.cs
	namespace 	
Producao
 
. 
Infrastructure !
.! "
Data" &
;& '
public		 
class		 
ProductionDbContext		  
{

 
private 
readonly 
IMongoDatabase #
	_database$ -
;- .
public 

ProductionDbContext 
( 
IMongoDatabase -
database. 6
)6 7
{ 
	_database 
= 
database 
; 
} 
public 

IMongoCollection 
< 
ProductionOrder +
>+ ,
ProductionOrders- =
=>> @
	_database 
. 
GetCollection 
<  
ProductionOrder  /
>/ 0
(0 1
$str1 C
)C D
;D E
} »
kD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Infrastructure\DependencyInjection.cs
	namespace 	
Producao
 
. 
Infrastructure !
;! "
public 
static 
class 
DependencyInjection '
{ 
public 

static 
IServiceCollection $
AddInfrastructure% 6
(6 7
this7 ;
IServiceCollection< N
servicesO W
,W X
IConfigurationY g
configurationh u
)u v
{ 
var 
connectionString 
= 
configuration ,
., -
GetConnectionString- @
(@ A
$strA Y
)Y Z
?? 
$str *
;* +
var 
databaseName 
= 
configuration (
[( )
$str) ?
]? @
??A C
$strD Y
;Y Z
var 
mongoClient 
= 
new 
MongoClient )
() *
connectionString* :
): ;
;; <
var 
mongoDatabase 
= 
mongoClient '
.' (
GetDatabase( 3
(3 4
databaseName4 @
)@ A
;A B
services 
. 
AddSingleton 
< 
IMongoDatabase ,
>, -
(- .
mongoDatabase. ;
); <
;< =
services 
. 
	AddScoped 
< 
ProductionDbContext .
>. /
(/ 0
)0 1
;1 2
services 
. 
	AddScoped 
< &
IProductionOrderRepository 5
,5 6%
ProductionOrderRepository7 P
>P Q
(Q R
)R S
;S T
services"" 
."" 
	AddScoped"" 
<""  
INotificationService"" /
,""/ 0$
EmailNotificationService""1 I
>""I J
(""J K
)""K L
;""L M
services%% 
.%% 
AddHttpClient%% 
<%% 
IOrderServiceClient%% 2
,%%2 3
OrderServiceClient%%4 F
>%%F G
(%%G H
)%%H I
;%%I J
return'' 
services'' 
;'' 
}(( 
})) 