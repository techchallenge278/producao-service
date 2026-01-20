›
qD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Domain\Shared\Exceptions\DomainException.cs
	namespace 	
Producao
 
. 
Domain 
. 
Shared  
.  !

Exceptions! +
;+ ,
public 
class 
DomainException 
: 
	Exception (
{ 
public 

DomainException 
( 
string !
message" )
)) *
:+ ,
base- 1
(1 2
message2 9
)9 :
{		 
}

 
public 

DomainException 
( 
string !
message" )
,) *
	Exception+ 4
innerException5 C
)C D
:E F
baseG K
(K L
messageL S
,S T
innerExceptionU c
)c d
{ 
} 
} Ñ
gD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Domain\Shared\Entities\IEntity.cs
	namespace 	
Producao
 
. 
Domain 
. 
Shared  
.  !
Entities! )
;) *
public 
	interface 
IEntity 
{ 
Guid 
Id	 
{ 
get 
; 
} 
DateTime		 
	CreatedAt		 
{		 
get		 
;		 
}		 
DateTime

 
?

 
	UpdatedAt

 
{

 
get

 
;

 
}

  
void 
SetUpdatedAt	 
( 
) 
; 
} Ú
oD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Domain\Shared\Repositories\IRepository.cs
	namespace 	
Producao
 
. 
Domain 
. 
Shared  
.  !
Repositories! -
;- .
public 
	interface 
IRepository 
< 
T 
> 
where  %
T& '
:( )
class* /
,/ 0
IEntity1 8
{		 
Task

 
<

 	
T

	 

?


 
>

 
GetByIdAsync

 
(

 
Guid

 
id

 !
,

! "
CancellationToken

# 4
cancellationToken

5 F
=

G H
default

I P
)

P Q
;

Q R
Task 
< 	
IEnumerable	 
< 
T 
> 
> 
GetAllAsync $
($ %
int% (

pageNumber) 3
=4 5
$num6 7
,7 8
int9 <
pageSize= E
=F G
$numH J
,J K
CancellationTokenL ]
cancellationToken^ o
=p q
defaultr y
)y z
;z {
Task 
< 	
T	 

>
 
CreateAsync 
( 
T 
entity  
,  !
CancellationToken" 3
cancellationToken4 E
=F G
defaultH O
)O P
;P Q
Task 
UpdateAsync	 
( 
T 
entity 
, 
CancellationToken 0
cancellationToken1 B
=C D
defaultE L
)L M
;M N
Task 
DeleteAsync	 
( 
T 
entity 
, 
CancellationToken 0
cancellationToken1 B
=C D
defaultE L
)L M
;M N
}  
fD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Domain\Shared\Entities\Entity.cs
	namespace 	
Producao
 
. 
Domain 
. 
Shared  
.  !
Entities! )
;) *
public 
abstract 
class 
Entity 
: 
IEntity &
{ 
public 

Guid 
Id 
{ 
get 
; 
	protected #
set$ '
;' (
}) *
public		 

DateTime		 
	CreatedAt		 
{		 
get		  #
;		# $
	protected		% .
set		/ 2
;		2 3
}		4 5
public

 

DateTime

 
?

 
	UpdatedAt

 
{

  
get

! $
;

$ %
	protected

& /
set

0 3
;

3 4
}

5 6
	protected 
Entity 
( 
) 
{ 
Id 

= 
Guid 
. 
NewGuid 
( 
) 
; 
	CreatedAt 
= 
DateTime 
. 
UtcNow #
;# $
} 
public 

void 
SetUpdatedAt 
( 
) 
{ 
	UpdatedAt 
= 
DateTime 
. 
UtcNow #
;# $
} 
public 

override 
bool 
Equals 
(  
object  &
?& '
obj( +
)+ ,
{ 
if 

( 
obj 
is 
not 
Entity 
other #
)# $
return 
false 
; 
if 

( 
ReferenceEquals 
( 
this  
,  !
other" '
)' (
)( )
return 
true 
; 
if 

( 
GetType 
( 
) 
!= 
other 
. 
GetType &
(& '
)' (
)( )
return   
false   
;   
return"" 
Id"" 
=="" 
other"" 
."" 
Id"" 
;"" 
}## 
public%% 

override%% 
int%% 
GetHashCode%% #
(%%# $
)%%$ %
{&& 
return'' 
Id'' 
.'' 
GetHashCode'' 
('' 
)'' 
;''  
}(( 
})) à
ÅD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Domain\ProductionOrders\ValueObjects\ProductionOrderItem.cs
	namespace 	
Producao
 
. 
Domain 
. 
ProductionOrders *
.* +
ValueObjects+ 7
;7 8
public 
class 
ProductionOrderItem  
{ 
public 

Guid 
	ProductId 
{ 
get 
;  
private! (
set) ,
;, -
}. /
public		 

string		 
ProductName		 
{		 
get		  #
;		# $
private		% ,
set		- 0
;		0 1
}		2 3
public

 

int

 
Quantity

 
{

 
get

 
;

 
private

 &
set

' *
;

* +
}

, -
public 

decimal 
	UnitPrice 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
private 
ProductionOrderItem 
(  
)  !
{ 
ProductName 
= 
string 
. 
Empty "
;" #
} 
private 
ProductionOrderItem 
(  
Guid  $
	productId% .
,. /
string0 6
productName7 B
,B C
intD G
quantityH P
,P Q
decimalR Y
	unitPriceZ c
)c d
{ 
	ProductId 
= 
	productId 
; 
ProductName 
= 
productName !
;! "
Quantity 
= 
quantity 
; 
	UnitPrice 
= 
	unitPrice 
; 
} 
public 

static 
ProductionOrderItem %
Create& ,
(, -
Guid- 1
	productId2 ;
,; <
string= C
productNameD O
,O P
intQ T
quantityU ]
,] ^
decimal_ f
	unitPriceg p
)p q
{ 
if 

( 
	productId 
== 
Guid 
. 
Empty #
)# $
throw 
new 
ArgumentException '
(' (
$str( F
,F G
nameofH N
(N O
	productIdO X
)X Y
)Y Z
;Z [
if 

( 
string 
. 
IsNullOrWhiteSpace %
(% &
productName& 1
)1 2
)2 3
throw   
new   
ArgumentException   '
(  ' (
$str  ( H
,  H I
nameof  J P
(  P Q
productName  Q \
)  \ ]
)  ] ^
;  ^ _
if"" 

("" 
quantity"" 
<="" 
$num"" 
)"" 
throw## 
new## 
ArgumentException## '
(##' (
$str##( J
,##J K
nameof##L R
(##R S
quantity##S [
)##[ \
)##\ ]
;##] ^
if%% 

(%% 
	unitPrice%% 
<=%% 
$num%% 
)%% 
throw&& 
new&& 
ArgumentException&& '
(&&' (
$str&&( K
,&&K L
nameof&&M S
(&&S T
	unitPrice&&T ]
)&&] ^
)&&^ _
;&&_ `
return(( 
new(( 
ProductionOrderItem(( &
(((& '
	productId((' 0
,((0 1
productName((2 =
,((= >
quantity((? G
,((G H
	unitPrice((I R
)((R S
;((S T
})) 
}** ‚
~D:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Domain\ProductionOrders\ValueObjects\ProductionStatus.cs
	namespace 	
Producao
 
. 
Domain 
. 
ProductionOrders *
.* +
ValueObjects+ 7
;7 8
public 
enum 
ProductionStatus 
{ 
Received 
= 
$num 
, 
InPreparation 
= 
$num 
, 
Ready 	
=
 
$num 
, 
	Completed 
= 
$num 
, 
	Cancelled 
= 
$num 
}   ∂
~D:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Domain\ProductionOrders\Services\INotificationService.cs
	namespace 	
Producao
 
. 
Domain 
. 
ProductionOrders *
.* +
Services+ 3
;3 4
public 
	interface  
INotificationService %
{ 
Task (
NotifyOrderStatusChangeAsync	 %
(% &
Guid& *
orderId+ 2
,2 3
string4 :
previousStatus; I
,I J
stringK Q
	newStatusR [
,[ \
Guid] a
?a b

customerIdc m
)m n
;n o
Task !
NotifyOrderReadyAsync	 
( 
Guid #
orderId$ +
,+ ,
Guid- 1
?1 2

customerId3 =
)= >
;> ?
} ⁄
àD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Domain\ProductionOrders\Repositories\IProductionOrderRepository.cs
	namespace 	
Producao
 
. 
Domain 
. 
ProductionOrders *
.* +
Repositories+ 7
;7 8
public

 
	interface

 &
IProductionOrderRepository

 +
:

, -
IRepository

. 9
<

9 :
ProductionOrder

: I
>

I J
{ 
Task 
< 	
ProductionOrder	 
? 
> 
GetByOrderIdAsync ,
(, -
Guid- 1
orderId2 9
,9 :
CancellationToken; L
cancellationTokenM ^
=_ `
defaulta h
)h i
;i j
Task 
< 	
IEnumerable	 
< 
ProductionOrder $
>$ %
>% &
GetByStatusAsync' 7
(7 8
ProductionStatus8 H
statusI O
,O P
intQ T

pageNumberU _
=` a
$numb c
,c d
inte h
pageSizei q
=r s
$numt v
,v w
CancellationToken	x â
cancellationToken
ä õ
=
ú ù
default
û •
)
• ¶
;
¶ ß
Task 
< 	
IEnumerable	 
< 
ProductionOrder $
>$ %
>% &&
GetOrdersInProductionAsync' A
(A B
intB E

pageNumberF P
=Q R
$numS T
,T U
intV Y
pageSizeZ b
=c d
$nume g
,g h
CancellationTokeni z
cancellationToken	{ å
=
ç é
default
è ñ
)
ñ ó
;
ó ò
Task 
< 	
IEnumerable	 
< 
ProductionOrder $
>$ %
>% &
GetReadyOrdersAsync' :
(: ;
int; >

pageNumber? I
=J K
$numL M
,M N
intO R
pageSizeS [
=\ ]
$num^ `
,` a
CancellationTokenb s
cancellationToken	t Ö
=
Ü á
default
à è
)
è ê
;
ê ë
}  g
yD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Domain\ProductionOrders\Entities\ProductionOrder.cs
	namespace 	
Producao
 
. 
Domain 
. 
ProductionOrders *
.* +
Entities+ 3
;3 4
public

 
class

 
ProductionOrder

 
:

 
Entity

 %
{ 
public 

Guid 
OrderId 
{ 
get 
; 
private &
set' *
;* +
}, -
public 

Guid 
? 

CustomerId 
{ 
get !
;! "
private# *
set+ .
;. /
}0 1
public 

string 
? 
CustomerName 
{  !
get" %
;% &
private' .
set/ 2
;2 3
}4 5
public 

decimal 

TotalPrice 
{ 
get  #
;# $
private% ,
set- 0
;0 1
}2 3
public 

ProductionStatus 
Status "
{# $
get% (
;( )
private* 1
set2 5
;5 6
}7 8
public 

List 
< 
ProductionOrderItem #
># $
Items% *
{+ ,
get- 0
;0 1
private2 9
set: =
;= >
}? @
public 

DateTime 
? 
	StartedAt 
{  
get! $
;$ %
private& -
set. 1
;1 2
}3 4
public 

DateTime 
? 
ReadyAt 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
public 

DateTime 
? 
CompletedAt  
{! "
get# &
;& '
private( /
set0 3
;3 4
}5 6
public 

int 
EstimatedMinutes 
{  !
get" %
;% &
private' .
set/ 2
;2 3
}4 5
private 
ProductionOrder 
( 
) 
{ 
Items 
= 
new 
List 
< 
ProductionOrderItem ,
>, -
(- .
). /
;/ 0
} 
private 
ProductionOrder 
( 
Guid 
orderId 
, 
Guid 
? 

customerId 
, 
string 
? 
customerName 
, 
decimal   

totalPrice   
,   
List!! 
<!! 
ProductionOrderItem!!  
>!!  !
items!!" '
)!!' (
{"" 
Id## 

=## 
Guid## 
.## 
NewGuid## 
(## 
)## 
;## 
OrderId$$ 
=$$ 
orderId$$ 
;$$ 

CustomerId%% 
=%% 

customerId%% 
;%%  
CustomerName&& 
=&& 
customerName&& #
;&&# $

TotalPrice'' 
='' 

totalPrice'' 
;''  
Items(( 
=(( 
items(( 
??(( 
new(( 
List(( !
<((! "
ProductionOrderItem((" 5
>((5 6
(((6 7
)((7 8
;((8 9
Status)) 
=)) 
ProductionStatus)) !
.))! "
Received))" *
;))* +
	CreatedAt** 
=** 
DateTime** 
.** 
UtcNow** #
;**# $
EstimatedMinutes++ 
=++ %
CalculateEstimatedMinutes++ 4
(++4 5
Items++5 :
)++: ;
;++; <
},, 
public.. 

static.. 
ProductionOrder.. !
Create.." (
(..( )
Guid// 
orderId// 
,// 
Guid00 
?00 

customerId00 
,00 
string11 
?11 
customerName11 
,11 
decimal22 

totalPrice22 
,22 
List33 
<33 
ProductionOrderItem33  
>33  !
items33" '
)33' (
{44 
if55 

(55 
orderId55 
==55 
Guid55 
.55 
Empty55 !
)55! "
throw66 
new66 
ArgumentException66 '
(66' (
$str66( D
,66D E
nameof66F L
(66L M
orderId66M T
)66T U
)66U V
;66V W
if88 

(88 
items88 
==88 
null88 
||88 
items88 "
.88" #
Count88# (
==88) +
$num88, -
)88- .
throw99 
new99 
ArgumentException99 '
(99' (
$str99( N
,99N O
nameof99P V
(99V W
items99W \
)99\ ]
)99] ^
;99^ _
return;; 
new;; 
ProductionOrder;; "
(;;" #
orderId;;# *
,;;* +

customerId;;, 6
,;;6 7
customerName;;8 D
,;;D E

totalPrice;;F P
,;;P Q
items;;R W
);;W X
;;;X Y
}<< 
public>> 

void>> 
StartPreparation>>  
(>>  !
)>>! "
{?? 
if@@ 

(@@ 
Status@@ 
!=@@ 
ProductionStatus@@ &
.@@& '
Received@@' /
)@@/ 0
throwAA 
newAA %
InvalidOperationExceptionAA /
(AA/ 0
$"AA0 2
$strAA2 _
{AA_ `
StatusAA` f
}AAf g
"AAg h
)AAh i
;AAi j
StatusCC 
=CC 
ProductionStatusCC !
.CC! "
InPreparationCC" /
;CC/ 0
	StartedAtDD 
=DD 
DateTimeDD 
.DD 
UtcNowDD #
;DD# $
SetUpdatedAtEE 
(EE 
)EE 
;EE 
}FF 
publicHH 

voidHH 
MarkAsReadyHH 
(HH 
)HH 
{II 
ifJJ 

(JJ 
StatusJJ 
!=JJ 
ProductionStatusJJ &
.JJ& '
InPreparationJJ' 4
)JJ4 5
throwKK 
newKK %
InvalidOperationExceptionKK /
(KK/ 0
$"KK0 2
$strKK2 _
{KK_ `
StatusKK` f
}KKf g
"KKg h
)KKh i
;KKi j
StatusMM 
=MM 
ProductionStatusMM !
.MM! "
ReadyMM" '
;MM' (
ReadyAtNN 
=NN 
DateTimeNN 
.NN 
UtcNowNN !
;NN! "
SetUpdatedAtOO 
(OO 
)OO 
;OO 
}PP 
publicRR 

voidRR 
CompleteRR 
(RR 
)RR 
{SS 
ifTT 

(TT 
StatusTT 
!=TT 
ProductionStatusTT &
.TT& '
ReadyTT' ,
)TT, -
throwUU 
newUU %
InvalidOperationExceptionUU /
(UU/ 0
$"UU0 2
$strUU2 V
{UUV W
StatusUUW ]
}UU] ^
"UU^ _
)UU_ `
;UU` a
StatusWW 
=WW 
ProductionStatusWW !
.WW! "
	CompletedWW" +
;WW+ ,
CompletedAtXX 
=XX 
DateTimeXX 
.XX 
UtcNowXX %
;XX% &
SetUpdatedAtYY 
(YY 
)YY 
;YY 
}ZZ 
public\\ 

void\\ 
Cancel\\ 
(\\ 
)\\ 
{]] 
if^^ 

(^^ 
Status^^ 
==^^ 
ProductionStatus^^ &
.^^& '
	Completed^^' 0
)^^0 1
throw__ 
new__ %
InvalidOperationException__ /
(__/ 0
$str__0 a
)__a b
;__b c
Statusaa 
=aa 
ProductionStatusaa !
.aa! "
	Cancelledaa" +
;aa+ ,
SetUpdatedAtbb 
(bb 
)bb 
;bb 
}cc 
publicee 

voidee 
UpdateStatusee 
(ee 
ProductionStatusee -
	newStatusee. 7
)ee7 8
{ff 
ifgg 

(gg 
Statusgg 
==gg 
ProductionStatusgg &
.gg& '
	Completedgg' 0
)gg0 1
throwhh 
newhh %
InvalidOperationExceptionhh /
(hh/ 0
$strhh0 g
)hhg h
;hhh i
Statusjj 
=jj 
	newStatusjj 
;jj 
SetUpdatedAtkk 
(kk 
)kk 
;kk 
ifmm 

(mm 
	newStatusmm 
==mm 
ProductionStatusmm )
.mm) *
InPreparationmm* 7
&&mm8 :
	StartedAtmm; D
==mmE G
nullmmH L
)mmL M
	StartedAtnn 
=nn 
DateTimenn  
.nn  !
UtcNownn! '
;nn' (
elseoo 
ifoo 
(oo 
	newStatusoo 
==oo 
ProductionStatusoo .
.oo. /
Readyoo/ 4
&&oo5 7
ReadyAtoo8 ?
==oo@ B
nullooC G
)ooG H
ReadyAtpp 
=pp 
DateTimepp 
.pp 
UtcNowpp %
;pp% &
elseqq 
ifqq 
(qq 
	newStatusqq 
==qq 
ProductionStatusqq .
.qq. /
	Completedqq/ 8
&&qq9 ;
CompletedAtqq< G
==qqH J
nullqqK O
)qqO P
CompletedAtrr 
=rr 
DateTimerr "
.rr" #
UtcNowrr# )
;rr) *
}ss 
privateuu 
staticuu 
intuu %
CalculateEstimatedMinutesuu 0
(uu0 1
Listuu1 5
<uu5 6
ProductionOrderItemuu6 I
>uuI J
itemsuuK P
)uuP Q
{vv 
ifww 

(ww 
itemsww 
==ww 
nullww 
||ww 
itemsww "
.ww" #
Countww# (
==ww) +
$numww, -
)ww- .
returnxx 
$numxx 
;xx 
var{{ 
baseMinutes{{ 
={{ 
items{{ 
.{{  
Count{{  %
*{{& '
$num{{( )
;{{) *
return|| 
Math|| 
.|| 
Max|| 
(|| 
baseMinutes|| #
,||# $
$num||% '
)||' (
;||( )
}}} 
public 

int !
GetWaitingTimeMinutes $
($ %
)% &
{
ÄÄ 
if
ÅÅ 

(
ÅÅ 
Status
ÅÅ 
==
ÅÅ 
ProductionStatus
ÅÅ &
.
ÅÅ& '
	Completed
ÅÅ' 0
&&
ÅÅ1 3
CompletedAt
ÅÅ4 ?
.
ÅÅ? @
HasValue
ÅÅ@ H
)
ÅÅH I
{
ÇÇ 	
return
ÉÉ 
(
ÉÉ 
int
ÉÉ 
)
ÉÉ 
(
ÉÉ 
CompletedAt
ÉÉ $
.
ÉÉ$ %
Value
ÉÉ% *
-
ÉÉ+ ,
	CreatedAt
ÉÉ- 6
)
ÉÉ6 7
.
ÉÉ7 8
TotalMinutes
ÉÉ8 D
;
ÉÉD E
}
ÑÑ 	
else
ÖÖ 
if
ÖÖ 
(
ÖÖ 
Status
ÖÖ 
==
ÖÖ 
ProductionStatus
ÖÖ +
.
ÖÖ+ ,
Ready
ÖÖ, 1
&&
ÖÖ2 4
ReadyAt
ÖÖ5 <
.
ÖÖ< =
HasValue
ÖÖ= E
)
ÖÖE F
{
ÜÜ 	
return
áá 
(
áá 
int
áá 
)
áá 
(
áá 
ReadyAt
áá  
.
áá  !
Value
áá! &
-
áá' (
	CreatedAt
áá) 2
)
áá2 3
.
áá3 4
TotalMinutes
áá4 @
;
áá@ A
}
àà 	
else
ââ 
if
ââ 
(
ââ 
	StartedAt
ââ 
.
ââ 
HasValue
ââ #
)
ââ# $
{
ää 	
return
ãã 
(
ãã 
int
ãã 
)
ãã 
(
ãã 
DateTime
ãã !
.
ãã! "
UtcNow
ãã" (
-
ãã) *
	StartedAt
ãã+ 4
.
ãã4 5
Value
ãã5 :
)
ãã: ;
.
ãã; <
TotalMinutes
ãã< H
;
ããH I
}
åå 	
else
çç 
{
éé 	
return
èè 
(
èè 
int
èè 
)
èè 
(
èè 
DateTime
èè !
.
èè! "
UtcNow
èè" (
-
èè) *
	CreatedAt
èè+ 4
)
èè4 5
.
èè5 6
TotalMinutes
èè6 B
;
èèB C
}
êê 	
}
ëë 
}íí 