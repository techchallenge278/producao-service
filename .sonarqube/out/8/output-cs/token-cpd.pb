 R
TD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Api\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
TimeZoneInfo 
brazilTimeZone 
; 
try 
{ 
brazilTimeZone 
= 
TimeZoneInfo !
.! ""
FindSystemTimeZoneById" 8
(8 9
$str9 Y
)Y Z
;Z [
} 
catch 
{ 
brazilTimeZone 
= 
TimeZoneInfo !
.! ""
FindSystemTimeZoneById" 8
(8 9
$str9 L
)L M
;M N
} 
	AppDomain 	
.	 

CurrentDomain
 
. 
SetData 
(  
$str  *
,* +
brazilTimeZone, :
): ;
;; <
var   
cultureInfo   
=   
new   
CultureInfo   !
(  ! "
$str  " )
)  ) *
;  * +
CultureInfo!! 
.!! '
DefaultThreadCurrentCulture!! '
=!!( )
cultureInfo!!* 5
;!!5 6
CultureInfo"" 
."" )
DefaultThreadCurrentUICulture"" )
=""* +
cultureInfo"", 7
;""7 8
BsonSerializer&& 
.&& 
RegisterSerializer&& !
(&&! "
new&&" %
GuidSerializer&&& 4
(&&4 5
GuidRepresentation&&5 G
.&&G H
Standard&&H P
)&&P Q
)&&Q R
;&&R S
builder** 
.** 
Services** 
.** 
AddInfrastructure** "
(**" #
builder**# *
.*** +
Configuration**+ 8
)**8 9
;**9 :
builder++ 
.++ 
Services++ 
.++ 
AddApplication++ 
(++  
)++  !
;++! "
builder.. 
... 
Services.. 
... 
AddAuthentication.. "
(.." #
JwtBearerDefaults..# 4
...4 5 
AuthenticationScheme..5 I
)..I J
.// 
AddJwtBearer// 
(// 
options// 
=>// 
{00 
var22 
	authority22 
=22 
builder22 
.22  
Configuration22  -
[22- .
$str22. >
]22> ?
;22? @
var33 
audience33 
=33 
builder33 
.33 
Configuration33 ,
[33, -
$str33- <
]33< =
;33= >
var55 
useExternalAuth55 
=55 
!66 
builder66 
.66 
Environment66  
.66  !
IsDevelopment66! .
(66. /
)66/ 0
&&661 3
!77 
string77 
.77 
IsNullOrWhiteSpace77 &
(77& '
	authority77' 0
)770 1
&&772 4
!88 
string88 
.88 
IsNullOrWhiteSpace88 &
(88& '
audience88' /
)88/ 0
;880 1
if:: 

(:: 
!:: 
useExternalAuth:: 
):: 
{;; 	
var<< 
	jwtSecret<< 
=<< 
builder<< #
.<<# $
Configuration<<$ 1
[<<1 2
$str<<2 >
]<<> ?
??== 
throw== 
new== %
InvalidOperationException== 6
(==6 7
$str==7 S
)==S T
;==T U
var?? 
	jwtIssuer?? 
=?? 
builder?? #
.??# $
Configuration??$ 1
[??1 2
$str??2 >
]??> ?
;??? @
var@@ 
jwtAudience@@ 
=@@ 
builder@@ %
.@@% &
Configuration@@& 3
[@@3 4
$str@@4 B
]@@B C
;@@C D
optionsBB 
.BB %
TokenValidationParametersBB -
=BB. /
newBB0 3%
TokenValidationParametersBB4 M
{CC 
ValidateIssuerDD 
=DD  
trueDD! %
,DD% &
ValidateAudienceEE  
=EE! "
trueEE# '
,EE' (
ValidateLifetimeFF  
=FF! "
trueFF# '
,FF' ($
ValidateIssuerSigningKeyGG (
=GG) *
trueGG+ /
,GG/ 0
ValidIssuerHH 
=HH 
	jwtIssuerHH '
,HH' (
ValidAudienceII 
=II 
jwtAudienceII  +
,II+ ,
IssuerSigningKeyJJ  
=JJ! "
newJJ# & 
SymmetricSecurityKeyJJ' ;
(JJ; <
EncodingKK 
.KK 
UTF8KK !
.KK! "
GetBytesKK" *
(KK* +
	jwtSecretKK+ 4
)KK4 5
)LL 
,LL 
	ClockSkewMM 
=MM 
TimeSpanMM $
.MM$ %
FromMinutesMM% 0
(MM0 1
$numMM1 2
)MM2 3
}NN 
;NN 
}OO 	
elsePP 
{QQ 	
optionsRR 
.RR 
	AuthorityRR 
=RR 
	authorityRR  )
;RR) *
optionsSS 
.SS 
AudienceSS 
=SS 
audienceSS '
;SS' (
}TT 	
}UU 
)UU 
;UU 
builderXX 
.XX 
ServicesXX 
.XX #
AddAuthorizationBuilderXX (
(XX( )
)XX) *
.YY 
	AddPolicyYY 
(YY 
$strYY 
,YY 
policyYY "
=>YY# %
{ZZ 
policy[[ 
.[[ $
RequireAuthenticatedUser[[ '
([[' (
)[[( )
;[[) *
policy\\ 
.\\ 
RequireAssertion\\ 
(\\  
context\\  '
=>\\( *
{]] 	
var^^ 
roles^^ 
=^^ 
context^^ 
.^^  
User^^  $
.^^$ %
FindAll^^% ,
(^^, -
$str^^- =
)^^= >
.__ 
Concat__ 
(__ 
context__ 
.__  
User__  $
.__$ %
FindAll__% ,
(__, -
$str__- 4
)__4 5
)__5 6
.`` 
Concat`` 
(`` 
context`` 
.``  
User``  $
.``$ %
FindAll``% ,
(``, -
$str``- 3
)``3 4
)``4 5
.aa 
Selectaa 
(aa 
caa 
=>aa 
caa 
.aa 
Valueaa $
)aa$ %
;aa% &
returncc 
rolescc 
.cc 
Anycc 
(cc 
rolecc !
=>cc" $
rolecc% )
.cc) *
Equalscc* 0
(cc0 1
$strcc1 8
,cc8 9
StringComparisoncc: J
.ccJ K
OrdinalIgnoreCaseccK \
)cc\ ]
||cc^ `
roledd# '
.dd' (
Equalsdd( .
(dd. /
$strdd/ >
,dd> ?
StringComparisondd@ P
.ddP Q
OrdinalIgnoreCaseddQ b
)ddb c
)ddc d
;ddd e
}ee 	
)ee	 

;ee
 
}ff 
)ff 
;ff 
builderii 
.ii 
Servicesii 
.ii 
AddHealthChecksii  
(ii  !
)ii! "
;ii" #
builderkk 
.kk 
Serviceskk 
.kk 
AddControllerskk 
(kk  
optionskk  '
=>kk( *
{ll 
optionsmm 
.mm 
Filtersmm 
.mm 
Addmm 
<mm '
ApiExceptionFilterAttributemm 3
>mm3 4
(mm4 5
)mm5 6
;mm6 7
}nn 
)nn 
;nn 
builderpp 
.pp 
Servicespp 
.pp #
AddEndpointsApiExplorerpp (
(pp( )
)pp) *
;pp* +
builderqq 
.qq 
Servicesqq 
.qq 
AddSwaggerGenqq 
(qq 
)qq  
;qq  !
varss 
appss 
=ss 	
builderss
 
.ss 
Buildss 
(ss 
)ss 
;ss 
appvv 
.vv 

UseSwaggervv 
(vv 
)vv 
;vv 
appww 
.ww 
UseSwaggerUIww 
(ww 
)ww 
;ww 
appyy 
.yy 
UseHttpsRedirectionyy 
(yy 
)yy 
;yy 
app{{ 
.{{ 
UseAuthentication{{ 
({{ 
){{ 
;{{ 
app|| 
.|| 
UseAuthorization|| 
(|| 
)|| 
;|| 
app~~ 
.~~ 
Use~~ 
(~~ 
async~~ 
(~~ 
context~~ 
,~~ 
next~~ 
)~~ 
=>~~  
{ 
if
ÄÄ 
(
ÄÄ 
string
ÄÄ 
.
ÄÄ 
IsNullOrEmpty
ÄÄ 
(
ÄÄ 
context
ÄÄ $
.
ÄÄ$ %
Request
ÄÄ% ,
.
ÄÄ, -
Path
ÄÄ- 1
)
ÄÄ1 2
||
ÄÄ3 5
context
ÄÄ6 =
.
ÄÄ= >
Request
ÄÄ> E
.
ÄÄE F
Path
ÄÄF J
==
ÄÄK M
$str
ÄÄN Q
)
ÄÄQ R
{
ÅÅ 
context
ÇÇ 
.
ÇÇ 
Response
ÇÇ 
.
ÇÇ 
Redirect
ÇÇ !
(
ÇÇ! "
$str
ÇÇ" 7
)
ÇÇ7 8
;
ÇÇ8 9
return
ÉÉ 
;
ÉÉ 
}
ÑÑ 
await
ÖÖ 	
next
ÖÖ
 
(
ÖÖ 
)
ÖÖ 
;
ÖÖ 
}ÜÜ 
)
ÜÜ 
;
ÜÜ 
appââ 
.
ââ 
MapHealthChecks
ââ 
(
ââ 
$str
ââ 
)
ââ 
;
ââ 
appãã 
.
ãã 
MapControllers
ãã 
(
ãã 
)
ãã 
;
ãã 
awaitçç 
app
çç 	
.
çç	 

RunAsync
çç
 
(
çç 
)
çç 
;
çç 
returnèè 
$num
èè 
;
èè 	
publicëë 
partial
ëë 
class
ëë 
Program
ëë 
{íí 
	protected
ìì 
Program
ìì 
(
ìì 
)
ìì 
{
ìì 
}
ìì 
}îî Ü
aD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Api\Models\ErrorResponse.cs
	namespace 	
Producao
 
. 
Api 
. 
Models 
; 
public 
class 
ErrorResponse 
{ 
public 

ErrorResponse 
( 
) 
{		 
Errors

 
=

 
new

 
List

 
<

 
string

  
>

  !
(

! "
)

" #
;

# $
} 
public 

ErrorResponse 
( 
string 
error  %
)% &
:' (
this) -
(- .
). /
{ 
Errors 
. 
Add 
( 
error 
) 
; 
} 
public 

ErrorResponse 
( 
IEnumerable $
<$ %
string% +
>+ ,
errors- 3
)3 4
:5 6
this7 ;
(; <
)< =
{ 
Errors 
. 
AddRange 
( 
errors 
) 
;  
} 
public 

List 
< 
string 
> 
Errors 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 

string 
? 
TraceId 
{ 
get  
;  !
set" %
;% &
}' (
} È
pD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Api\DTOs\UpdateProductionOrderStatusDto.cs
	namespace 	
Producao
 
. 
Api 
. 
DTOs 
; 
public 
class *
UpdateProductionOrderStatusDto +
{ 
[ 
Required 
( 
ErrorMessage 
= 
$str 3
)3 4
]4 5
public 

required 
string 
Status !
{" #
get$ '
;' (
set) ,
;, -
}. /
=0 1
string2 8
.8 9
Empty9 >
;> ?
}		 ·=
pD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Api\Filters\ApiExceptionFilterAttribute.cs
	namespace 	
Producao
 
. 
Api 
. 
Filters 
; 
[ 
AttributeUsage 
( 
AttributeTargets  
.  !
Class! &
|' (
AttributeTargets) 9
.9 :
Method: @
)@ A
]A B
public		 
class		 '
ApiExceptionFilterAttribute		 (
:		) *$
ExceptionFilterAttribute		+ C
{

 
public 

override 
void 
OnException $
($ %
ExceptionContext% 5
context6 =
)= >
{ 
HandleException 
( 
context 
)  
;  !
base 
. 
OnException 
( 
context  
)  !
;! "
} 
private 
static 
void 
HandleException '
(' (
ExceptionContext( 8
context9 @
)@ A
{ 
var 
	exception 
= 
context 
.  
	Exception  )
;) *
var 
problemDetails 
= 
new  
ProblemDetails! /
(/ 0
)0 1
;1 2
switch 
( 
	exception 
) 
{ 	
case 
NotFoundException "
notFoundException# 4
:4 5#
HandleNotFoundException '
(' (
context( /
,/ 0
notFoundException1 B
,B C
problemDetailsD R
)R S
;S T
break 
; 
case 
ValidationException $
validationException% 8
:8 9%
HandleValidationException )
() *
context* 1
,1 2
validationException3 F
,F G
problemDetailsH V
)V W
;W X
break 
; 
case   
DomainException    
domainException  ! 0
:  0 1!
HandleDomainException!! %
(!!% &
context!!& -
,!!- .
domainException!!/ >
,!!> ?
problemDetails!!@ N
)!!N O
;!!O P
break"" 
;"" 
default$$ 
:$$ "
HandleUnknownException%% &
(%%& '
context%%' .
,%%. /
	exception%%0 9
,%%9 :
problemDetails%%; I
)%%I J
;%%J K
break&& 
;&& 
}'' 	
context)) 
.)) 
Result)) 
=)) 
new)) 
ObjectResult)) )
())) *
problemDetails))* 8
)))8 9
{** 	

StatusCode++ 
=++ 
problemDetails++ '
.++' (
Status++( .
},, 	
;,,	 

context-- 
.-- 
ExceptionHandled--  
=--! "
true--# '
;--' (
}.. 
private00 
static00 
void00 #
HandleNotFoundException00 /
(00/ 0
ExceptionContext000 @
context00A H
,00H I
NotFoundException00J [
	exception00\ e
,00e f
ProblemDetails00g u
problemDetails	00v Ñ
)
00Ñ Ö
{11 
problemDetails22 
.22 
Status22 
=22 
StatusCodes22  +
.22+ ,
Status404NotFound22, =
;22= >
problemDetails33 
.33 
Title33 
=33 
	exception33 (
.33( )
Message33) 0
;330 1
problemDetails44 
.44 
Type44 
=44 
$str44 (
;44( )
problemDetails55 
.55 
Detail55 
=55 
	exception55  )
.55) *
Message55* 1
;551 2
context66 
.66 
HttpContext66 
.66 
Response66 $
.66$ %

StatusCode66% /
=660 1
StatusCodes662 =
.66= >
Status404NotFound66> O
;66O P
}77 
private99 
static99 
void99 %
HandleValidationException99 1
(991 2
ExceptionContext992 B
context99C J
,99J K
ValidationException99L _
	exception99` i
,99i j
ProblemDetails99k y
problemDetails	99z à
)
99à â
{:: 
problemDetails;; 
.;; 
Status;; 
=;; 
StatusCodes;;  +
.;;+ ,
Status400BadRequest;;, ?
;;;? @
problemDetails<< 
.<< 
Title<< 
=<< 
	exception<< (
.<<( )
Message<<) 0
;<<0 1
problemDetails== 
.== 
Type== 
=== 
$str== 1
;==1 2
problemDetails>> 
.>> 
Detail>> 
=>> 
	exception>>  )
.>>) *
Message>>* 1
;>>1 2
context?? 
.?? 
HttpContext?? 
.?? 
Response?? $
.??$ %

StatusCode??% /
=??0 1
StatusCodes??2 =
.??= >
Status400BadRequest??> Q
;??Q R
}@@ 
privateBB 
staticBB 
voidBB !
HandleDomainExceptionBB -
(BB- .
ExceptionContextBB. >
contextBB? F
,BBF G
DomainExceptionBBH W
	exceptionBBX a
,BBa b
ProblemDetailsBBc q
problemDetails	BBr Ä
)
BBÄ Å
{CC 
problemDetailsDD 
.DD 
StatusDD 
=DD 
StatusCodesDD  +
.DD+ ,
Status400BadRequestDD, ?
;DD? @
problemDetailsEE 
.EE 
TitleEE 
=EE 
	exceptionEE (
.EE( )
MessageEE) 0
;EE0 1
problemDetailsFF 
.FF 
TypeFF 
=FF 
$strFF +
;FF+ ,
problemDetailsGG 
.GG 
DetailGG 
=GG 
	exceptionGG  )
.GG) *
MessageGG* 1
;GG1 2
contextHH 
.HH 
HttpContextHH 
.HH 
ResponseHH $
.HH$ %

StatusCodeHH% /
=HH0 1
StatusCodesHH2 =
.HH= >
Status400BadRequestHH> Q
;HHQ R
}II 
privateKK 
staticKK 
voidKK "
HandleUnknownExceptionKK .
(KK. /
ExceptionContextKK/ ?
contextKK@ G
,KKG H
	ExceptionKKI R
	exceptionKKS \
,KK\ ]
ProblemDetailsKK^ l
problemDetailsKKm {
)KK{ |
{LL 
problemDetailsMM 
.MM 
StatusMM 
=MM 
StatusCodesMM  +
.MM+ ,(
Status500InternalServerErrorMM, H
;MMH I
problemDetailsNN 
.NN 
TitleNN 
=NN 
$strNN M
;NNM N
problemDetailsOO 
.OO 
TypeOO 
=OO 
$strOO +
;OO+ ,
problemDetailsPP 
.PP 
DetailPP 
=PP 
	exceptionPP  )
.PP) *
MessagePP* 1
;PP1 2
contextQQ 
.QQ 
HttpContextQQ 
.QQ 
ResponseQQ $
.QQ$ %

StatusCodeQQ% /
=QQ0 1
StatusCodesQQ2 =
.QQ= >(
Status500InternalServerErrorQQ> Z
;QQZ [
}RR 
}SS ≤ 
jD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Api\DTOs\CreateProductionOrderDto.cs
	namespace 	
Producao
 
. 
Api 
. 
DTOs 
; 
public 
class $
CreateProductionOrderDto %
{ 
[ 
Required 
( 
ErrorMessage 
= 
$str 4
)4 5
]5 6
public 

required 
Guid 
OrderId  
{! "
get# &
;& '
set( +
;+ ,
}- .
public

 

Guid

 
?

 

CustomerId

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
public 

string 
? 
CustomerName 
{  !
get" %
;% &
set' *
;* +
}, -
[ 
Required 
( 
ErrorMessage 
= 
$str 7
)7 8
]8 9
[ 
Range 

(
 
$num 
, 
double 
. 
MaxValue  
,  !
ErrorMessage" .
=/ 0
$str1 U
)U V
]V W
public 

required 
decimal 

TotalPrice &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
[ 
Required 
( 
ErrorMessage 
= 
$str 5
)5 6
]6 7
[ 
	MinLength 
( 
$num 
, 
ErrorMessage 
=  
$str! G
)G H
]H I
public 

required 
List 
< (
CreateProductionOrderItemDto 5
>5 6
Items7 <
{= >
get? B
;B C
setD G
;G H
}I J
=K L
newM P
(P Q
)Q R
;R S
} 
public 
class (
CreateProductionOrderItemDto )
{ 
[ 
Required 
( 
ErrorMessage 
= 
$str 6
)6 7
]7 8
public 

required 
Guid 
	ProductId "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 
Required 
( 
ErrorMessage 
= 
$str 8
)8 9
]9 :
public 

required 
string 
ProductName &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
=5 6
string7 =
.= >
Empty> C
;C D
[ 
Required 
( 
ErrorMessage 
= 
$str 5
)5 6
]6 7
[   
Range   

(  
 
$num   
,   
int   
.   
MaxValue   
,   
ErrorMessage   (
=  ) *
$str  + M
)  M N
]  N O
public!! 

required!! 
int!! 
Quantity!!  
{!!! "
get!!# &
;!!& '
set!!( +
;!!+ ,
}!!- .
[## 
Required## 
(## 
ErrorMessage## 
=## 
$str## 6
)##6 7
]##7 8
[$$ 
Range$$ 

($$
 
$num$$ 
,$$ 
double$$ 
.$$ 
MaxValue$$  
,$$  !
ErrorMessage$$" .
=$$/ 0
$str$$1 T
)$$T U
]$$U V
public%% 

required%% 
decimal%% 
	UnitPrice%% %
{%%& '
get%%( +
;%%+ ,
set%%- 0
;%%0 1
}%%2 3
}&& úd
mD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Api\Controllers\ProductionController.cs
	namespace

 	
Producao


 
.

 
Api

 
.

 
Controllers

 "
;

" #
[ 
ApiController 
] 
[ 
Route 
( 
$str 
) 
] 
[ 
	Authorize 

]
 
public 
class  
ProductionController !
:" #
ControllerBase$ 2
{ 
private 
readonly 
	IMediator 
	_mediator (
;( )
private   
readonly   
ILogger   
<    
ProductionController   1
>  1 2
_logger  3 :
;  : ;
public"" 
 
ProductionController"" 
(""  
	IMediator""  )
mediator""* 2
,""2 3
ILogger""4 ;
<""; < 
ProductionController""< P
>""P Q
logger""R X
)""X Y
{## 
	_mediator$$ 
=$$ 
mediator$$ 
;$$ 
_logger%% 
=%% 
logger%% 
;%% 
}&& 
[++ 
HttpPost++ 
(++ 
$str++ 
)++ 
]++ 
[,, 
AllowAnonymous,, 
],, 
[..  
ProducesResponseType.. 
(.. 
typeof..  
(..  !.
"CreateProductionOrderCommandResult..! C
)..C D
,..D E
StatusCodes..F Q
...Q R
Status201Created..R b
)..b c
]..c d
[//  
ProducesResponseType// 
(// 
typeof//  
(//  !
ErrorResponse//! .
)//. /
,/// 0
StatusCodes//1 <
.//< =
Status400BadRequest//= P
)//P Q
]//Q R
public00 

async00 
Task00 
<00 
IActionResult00 #
>00# $!
CreateProductionOrder00% :
(00: ;
[00; <
FromBody00< D
]00D E$
CreateProductionOrderDto00F ^
request00_ f
)00f g
{11 
_logger22 
.22 
LogInformation22 
(22 
$str22 R
,22R S
request22T [
.22[ \
OrderId22\ c
)22c d
;22d e
var44 
command44 
=44 
new44 (
CreateProductionOrderCommand44 6
{55 	
OrderId66 
=66 
request66 
.66 
OrderId66 %
,66% &

CustomerId77 
=77 
request77  
.77  !

CustomerId77! +
,77+ ,
CustomerName88 
=88 
request88 "
.88" #
CustomerName88# /
,88/ 0

TotalPrice99 
=99 
request99  
.99  !

TotalPrice99! +
,99+ ,
Items:: 
=:: 
request:: 
.:: 
Items:: !
.::! "
Select::" (
(::( )
item::) -
=>::. 0
new::1 4,
 CreateProductionOrderItemCommand::5 U
{;; 
	ProductId<< 
=<< 
item<<  
.<<  !
	ProductId<<! *
,<<* +
ProductName== 
=== 
item== "
.==" #
ProductName==# .
,==. /
Quantity>> 
=>> 
item>> 
.>>  
Quantity>>  (
,>>( )
	UnitPrice?? 
=?? 
item??  
.??  !
	UnitPrice??! *
}@@ 
)@@ 
.@@ 
ToList@@ 
(@@ 
)@@ 
}AA 	
;AA	 

varCC 
resultCC 
=CC 
awaitCC 
	_mediatorCC $
.CC$ %
SendCC% )
(CC) *
commandCC* 1
)CC1 2
;CC2 3
returnEE 
CreatedAtActionEE 
(EE 
nameofEE %
(EE% &
GetByOrderIdEE& 2
)EE2 3
,EE3 4
newEE5 8
{EE9 :
orderIdEE; B
=EEC D
resultEEE K
.EEK L
OrderIdEEL S
}EET U
,EEU V
resultEEW ]
)EE] ^
;EE^ _
}FF 
[KK 
HttpGetKK 
(KK 
$strKK 
)KK 
]KK 
[LL 
AllowAnonymousLL 
]LL 
[NN  
ProducesResponseTypeNN 
(NN 
typeofNN  
(NN  !,
 GetOrdersInProductionQueryResultNN! A
)NNA B
,NNB C
StatusCodesNND O
.NNO P
Status200OKNNP [
)NN[ \
]NN\ ]
publicOO 

asyncOO 
TaskOO 
<OO 
IActionResultOO #
>OO# $!
GetOrdersInProductionOO% :
(OO: ;
[PP 	
	FromQueryPP	 
]PP 
stringPP 
?PP 
statusPP "
,PP" #
[QQ 	
	FromQueryQQ	 
]QQ 
intQQ 

pageNumberQQ "
=QQ# $
$numQQ% &
,QQ& '
[RR 	
	FromQueryRR	 
]RR 
intRR 
pageSizeRR  
=RR! "
$numRR# %
)RR% &
{SS 
_loggerTT 
.TT 
LogInformationTT 
(TT 
$strTT _
,TT_ `
statusTTa g
,TTg h

pageNumberTTi s
)TTs t
;TTt u
varVV 
queryVV 
=VV 
newVV &
GetOrdersInProductionQueryVV 2
{WW 	
StatusXX 
=XX 
statusXX 
,XX 

PageNumberYY 
=YY 

pageNumberYY #
,YY# $
PageSizeZZ 
=ZZ 
pageSizeZZ 
}[[ 	
;[[	 

var]] 
result]] 
=]] 
await]] 
	_mediator]] $
.]]$ %
Send]]% )
(]]) *
query]]* /
)]]/ 0
;]]0 1
return__ 
Ok__ 
(__ 
result__ 
)__ 
;__ 
}`` 
[ee 
HttpGetee 
(ee 
$stree $
)ee$ %
]ee% &
[ff 
AllowAnonymousff 
]ff 
[hh  
ProducesResponseTypehh 
(hh 
typeofhh  
(hh  !
ProductionOrderDtohh! 3
)hh3 4
,hh4 5
StatusCodeshh6 A
.hhA B
Status200OKhhB M
)hhM N
]hhN O
[ii  
ProducesResponseTypeii 
(ii 
typeofii  
(ii  !
ErrorResponseii! .
)ii. /
,ii/ 0
StatusCodesii1 <
.ii< =
Status404NotFoundii= N
)iiN O
]iiO P
publicjj 

asyncjj 
Taskjj 
<jj 
IActionResultjj #
>jj# $
GetByOrderIdjj% 1
(jj1 2
[jj2 3
	FromRoutejj3 <
]jj< =
Guidjj> B
orderIdjjC J
)jjJ K
{kk 
_loggerll 
.ll 
LogInformationll 
(ll 
$strll S
,llS T
orderIdllU \
)ll\ ]
;ll] ^
varnn 
querynn 
=nn 
newnn ,
 GetProductionOrderByOrderIdQuerynn 8
{nn9 :
OrderIdnn; B
=nnC D
orderIdnnE L
}nnM N
;nnN O
varoo 
resultoo 
=oo 
awaitoo 
	_mediatoroo $
.oo$ %
Sendoo% )
(oo) *
queryoo* /
)oo/ 0
;oo0 1
ifqq 

(qq 
!qq 
resultqq 
.qq 
Successqq 
)qq 
{rr 	
returnss 
NotFoundss 
(ss 
newss 
ErrorResponsess  -
{ss. /
Errorsss0 6
=ss7 8
newss9 <
Listss= A
<ssA B
stringssB H
>ssH I
{ssJ K
resultssL R
.ssR S
ErrorssS X
??ssY [
$strss\ s
}sst u
}ssv w
)ssw x
;ssx y
}tt 	
returnvv 
Okvv 
(vv 
resultvv 
.vv 
Ordervv 
)vv 
;vv  
}ww 
[|| 
HttpPut|| 
(|| 
$str|| +
)||+ ,
]||, -
[}} 
AllowAnonymous}} 
]}} 
[  
ProducesResponseType 
( 
typeof  
(  !4
(UpdateProductionOrderStatusCommandResult! I
)I J
,J K
StatusCodesL W
.W X
Status200OKX c
)c d
]d e
[
ÄÄ "
ProducesResponseType
ÄÄ 
(
ÄÄ 
typeof
ÄÄ  
(
ÄÄ  !
ErrorResponse
ÄÄ! .
)
ÄÄ. /
,
ÄÄ/ 0
StatusCodes
ÄÄ1 <
.
ÄÄ< =!
Status400BadRequest
ÄÄ= P
)
ÄÄP Q
]
ÄÄQ R
[
ÅÅ "
ProducesResponseType
ÅÅ 
(
ÅÅ 
typeof
ÅÅ  
(
ÅÅ  !
ErrorResponse
ÅÅ! .
)
ÅÅ. /
,
ÅÅ/ 0
StatusCodes
ÅÅ1 <
.
ÅÅ< =
Status404NotFound
ÅÅ= N
)
ÅÅN O
]
ÅÅO P
public
ÇÇ 

async
ÇÇ 
Task
ÇÇ 
<
ÇÇ 
IActionResult
ÇÇ #
>
ÇÇ# $
UpdateStatus
ÇÇ% 1
(
ÇÇ1 2
[
ÇÇ2 3
	FromRoute
ÇÇ3 <
]
ÇÇ< =
Guid
ÇÇ> B
orderId
ÇÇC J
,
ÇÇJ K
[
ÇÇL M
FromBody
ÇÇM U
]
ÇÇU V,
UpdateProductionOrderStatusDto
ÇÇW u
request
ÇÇv }
)
ÇÇ} ~
{
ÉÉ 
_logger
ÑÑ 
.
ÑÑ 
LogInformation
ÑÑ 
(
ÑÑ 
$str
ÑÑ U
,
ÑÑU V
orderId
ÑÑW ^
,
ÑÑ^ _
request
ÑÑ` g
.
ÑÑg h
Status
ÑÑh n
)
ÑÑn o
;
ÑÑo p
try
ÜÜ 
{
áá 	
var
àà 
command
àà 
=
àà 
new
àà 0
"UpdateProductionOrderStatusCommand
àà @
{
ââ 
OrderId
ää 
=
ää 
orderId
ää !
,
ää! "
Status
ãã 
=
ãã 
request
ãã  
.
ãã  !
Status
ãã! '
}
åå 
;
åå 
var
éé 
result
éé 
=
éé 
await
éé 
	_mediator
éé (
.
éé( )
Send
éé) -
(
éé- .
command
éé. 5
)
éé5 6
;
éé6 7
return
êê 
Ok
êê 
(
êê 
result
êê 
)
êê 
;
êê 
}
ëë 	
catch
íí 
(
íí 
NotFoundException
íí  
ex
íí! #
)
íí# $
{
ìì 	
return
îî 
NotFound
îî 
(
îî 
new
îî 
ErrorResponse
îî  -
{
îî. /
Errors
îî0 6
=
îî7 8
new
îî9 <
List
îî= A
<
îîA B
string
îîB H
>
îîH I
{
îîJ K
ex
îîL N
.
îîN O
Message
îîO V
}
îîW X
}
îîY Z
)
îîZ [
;
îî[ \
}
ïï 	
catch
ññ 
(
ññ !
ValidationException
ññ "
ex
ññ# %
)
ññ% &
{
óó 	
return
òò 

BadRequest
òò 
(
òò 
new
òò !
ErrorResponse
òò" /
{
òò0 1
Errors
òò2 8
=
òò9 :
new
òò; >
List
òò? C
<
òòC D
string
òòD J
>
òòJ K
{
òòL M
ex
òòN P
.
òòP Q
Message
òòQ X
}
òòY Z
}
òò[ \
)
òò\ ]
;
òò] ^
}
ôô 	
catch
öö 
(
öö 
	Exception
öö 
ex
öö 
)
öö 
{
õõ 	
_logger
úú 
.
úú 
LogError
úú 
(
úú 
ex
úú 
,
úú  
$str
úú! O
,
úúO P
orderId
úúQ X
)
úúX Y
;
úúY Z
return
ùù 

StatusCode
ùù 
(
ùù 
$num
ùù !
,
ùù! "
new
ùù# &
ErrorResponse
ùù' 4
{
ùù5 6
Errors
ùù7 =
=
ùù> ?
new
ùù@ C
List
ùùD H
<
ùùH I
string
ùùI O
>
ùùO P
{
ùùQ R
$str
ùùS r
}
ùùs t
}
ùùu v
)
ùùv w
;
ùùw x
}
ûû 	
}
üü 
}†† 