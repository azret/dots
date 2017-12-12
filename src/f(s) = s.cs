﻿using System;
using System.Dots;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class App {
    static void Inspect(string header, Dot[] A, ConsoleColor color, bool verbose) {
        Console.Write($"{header}: ");
        Console.ForegroundColor = color;
        Console.Write(Dots.decode(A));
        Console.ResetColor();
        Console.WriteLine();
    }

    static void Test(Dot[] X, Dot[][] ℳ, bool verbose) {
        var Y = Dots.compute(ℳ, X);
        Inspect("X", X, ConsoleColor.Green, verbose);
        Inspect("Y", Y, ConsoleColor.Yellow, verbose);
    }

    class Gram {
        public string gram;
    }

    static List<Gram> Load(out int max) {
        List<Gram> grams = new List<Gram>();

        char[] breaks = new char[] { '/', '\\', '\'', '"',  ',', ';', '.', '?', '!', ' ', '[', ']', '{', '}', '(', ')', '\r', '\n', '\t',
            '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        max = 3;

        foreach (string g in AMICITIA.Split(breaks, StringSplitOptions.RemoveEmptyEntries)) {
            if (!string.IsNullOrWhiteSpace(g)) {
                max = Math.Max(max, g.Length);
                grams.Add(new Gram() {
                    gram = g,
                });
            }
        }

        return grams;
    }

    static void Main(string[] args) {
        int MAX;

        var Ꝙ = Load(out MAX);

        MAX = (int)(MAX * 1.3);

        Dot[][] ℳ = new Dot[][]
        {
           Dots.create<ReLU>(MAX)
        };

        Dots.connect(ℳ, MAX);

        bool canceled = false;

        Console.CancelKeyPress += (sender, e) => {

            if (canceled) {
                Process.GetCurrentProcess().Kill();
            }

            e.Cancel = canceled = true;

        };

        int iter = 0;

        for (var t = 0; t < 3797; t++) {

            List<Dot[][]> P = new List<Dot[][]>();

            for (int p = 0; p < Environment.ProcessorCount * 7; p++) {
                P.Add(ℳ.copy());
            }

            Parallel.ForEach(P, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, (H, state) => {

                Random randomizer = new Random();

                double ε = 0.0;

                for (var k = 0; k < 1307; k++) {

                    int n = randomizer.Next(0, Ꝙ.Count);

                    var ξn = Dots.encode(MAX, Ꝙ[n].gram);

                    H.compute(ξn);

                    string s = Ꝙ[n].gram;

                    var Ꝙn = Dots.encode(MAX, s);

                    double e;

                    ε += e = H.sgd(

                        Ꝙn,

                        rate: 1.0 / (MAX),

                        momentum: 1 - 1.0 / MAX

                    );

                    System.Threading.Interlocked.Increment(ref iter);

                    e = ε / (k + 1);

                    if (iter % 1024 == 0)
                        Console.WriteLine($"{iter:n0}: {e} {s}");

                    if (canceled || e <= double.Epsilon || double.IsNaN(e) || double.IsInfinity(e)) {
                        state.Break();
                        break;
                    }
                }

            });

            ℳ.multiply(0.0);

            foreach (var H in P) {
                ℳ.add(H);
            }

            ℳ.multiply(1 / (double)P.Count);

            if (canceled) break;

        }

        for (var k = 0; k < 31; k++) {
            Test(Dots.encode(MAX, Ꝙ[Dots.random(0, Ꝙ.Count)].gram), ℳ, verbose: false);
        }

        ℳ.print(Console.Out);

        Console.ReadKey();
    }

    static string AMICITIA = @"

[1] Q. Mucius augur multa narrare de C. Laelio socero suo memoriter et iucunde solebat nec dubitare illum in omni sermone appellare sapientem; ego autem a patre ita eram deductus ad Scaevolam sumpta virili toga, ut, quoad possem et liceret, a senis latere numquam discederem; itaque multa ab eo prudenter disputata, multa etiam breviter et commode dicta memoriae mandabam fierique studebam eius prudentia doctior. Quo mortuo me ad pontificem Scaevolam contuli, quem unum nostrae civitatis et ingenio et iustitia praestantissimum audeo dicere. Sed de hoc alias; nunc redeo ad augurem.

[2] Cum saepe multa, tum memini domi in hemicyclio sedentem, ut solebat, cum et ego essem una et pauci admodum familiares, in eum sermonem illum incidere qui tum forte multis erat in ore. Meministi enim profecto, Attice, et eo magis, quod P. Sulpicio utebare multum, cum is tribunus plebis capitali odio a Q. Pompeio, qui tum erat consul, dissideret, quocum coniunctissime et amantissime vixerat, quanta esset hominum vel admiratio vel querella.

[3] Itaque tum Scaevola cum in eam ipsam mentionem incidisset, exposuit nobis sermonem Laeli de amicitia habitum ab illo secum et cum altero genero, C. Fannio Marci filio, paucis diebus post mortem Africani. Eius disputationis sententias memoriae mandavi, quas hoc libro exposui arbitratu meo; quasi enim ipsos induxi loquentes, ne 'inquam' et 'inquit' saepius interponeretur, atque ut tamquam a praesentibus coram haberi sermo videretur.

[4] Cum enim saepe mecum ageres ut de amicitia scriberem aliquid, digna mihi res cum omnium cognitione tum nostra familiaritate visa est. Itaque feci non invitus ut prodessem multis rogatu tuo. Sed ut in Catone Maiore, qui est scriptus ad te de senectute, Catonem induxi senem disputantem, quia nulla videbatur aptior persona quae de illa aetate loqueretur quam eius qui et diutissime senex fuisset et in ipsa senectute praeter ceteros floruisset, sic cum accepissemus a patribus maxime memorabilem C. Laeli et P. Scipionis familiaritatem fuisse, idonea mihi Laeli persona visa est quae de amicitia ea ipsa dissereret quae disputata ab eo meminisset Scaevola. Genus autem hoc sermonum positum in hominum veterum auctoritate, et eorum inlustrium, plus nescio quo pacto videtur habere gravitatis; itaque ipse mea legens sic afficior interdum ut Catonem, non me loqui existimem.

[5] Sed ut tum ad senem senex de senectute, sic hoc libro ad amicum amicissimus scripsi de amicitia. Tum est Cato locutus, quo erat nemo fere senior temporibus illis, nemo prudentior; nunc Laelius et sapiens (sic enim est habitus) et amicitiae gloria excellens de amicitia loquetur. Tu velim a me animum parumper avertas, Laelium loqui ipsum putes. C. Fannius et Q. Mucius ad socerum veniunt post mortem Africani; ab his sermo oritur, respondet Laelius, cuius tota disputatio est de amicitia, quam legens te ipse cognosces.

[6] Fannius: Sunt ista, Laeli; nec enim melior vir fuit Africano quisquam nec clarior. Sed existimare debes omnium oculos in te esse coniectos unum; te sapientem et appellant et existimant. Tribuebatur hoc modo M. Catoni; scimus L. Acilium apud patres nostros appellatum esse sapientem; sed uterque alio quodam modo, Acilius, quia prudens esse in iure civili putabatur, Cato, quia multarum rerum usum habebat; multa eius et in senatu et in foro vel provisa prudenter vel acta constanter vel responsa acute ferebantur; propterea quasi cognomen iam habebat in senectute sapientis.

[7] Te autem alio quodam modo non solum natura et moribus, verum etiam studio et doctrina esse sapientem, nec sicut vulgus, sed ut eruditi solent appellare sapientem, qualem in reliqua Graecia neminem (nam qui septem appellantur, eos, qui ista subtilius quaerunt, in numero sapientium non habent), Athenis unum accepimus, et eum quidem etiam Apollinis oraculo sapientissimum iudicatum; hanc esse in te sapientiam existimant, ut omnia tua in te posita esse ducas humanosque casus virtute inferiores putes. Itaque ex me quaerunt, credo ex hoc item Scaevola, quonam pacto mortem Africani feras, eoque magis quod proximis Nonis cum in hortos D. Bruti auguris commentandi causa, ut adsolet, venissemus, tu non adfuisti, qui diligentissime semper illum diem et illud munus solitus esses obire.

[8] Scaevola: Quaerunt quidem, C. Laeli, multi, ut est a Fannio dictum, sed ego id respondeo, quod animum adverti, te dolorem, quem acceperis cum summi viri tum amicissimi morte, ferre moderate nec potuisse non commoveri nec fuisse id humanitatis tuae; quod autem Nonis in collegio nostro non adfuisses, valetudinem respondeo causam, non maestitiam fuisse.

Laelius: Recte tu quidem, Scaevola, et vere; nec enim ab isto officio, quod semper usurpavi, cum valerem, abduci incommodo meo debui, nec ullo casu arbitror hoc constanti homini posse contingere, ut ulla intermissio fiat officii.

[9] Tu autem, Fanni, quod mihi tantum tribui dicis quantum ego nec adgnosco nec postulo, facis amice; sed, ut mihi videris, non recte iudicas de Catone; aut enim nemo, quod quidem magis credo, aut si quisquam, ille sapiens fuit. Quo modo, ut alia omittam, mortem filii tulit! memineram Paulum, videram Galum, sed hi in pueris, Cato in perfecto et spectato viro.

[10] Quam ob rem cave Catoni anteponas ne istum quidem ipsum, quem Apollo, ut ais, sapientissimum iudicavit; huius enim facta, illius dicta laudantur. De me autem, ut iam cum utroque vestrum loquar, sic habetote:

Ego si Scipionis desiderio me moveri negem, quam id recte faciam, viderint sapientes; sed certe mentiar. Moveor enim tali amico orbatus qualis, ut arbitror, nemo umquam erit, ut confirmare possum, nemo certe fuit; sed non egeo medicina, me ipse consolor et maxime illo solacio quod eo errore careo quo amicorum decessu plerique angi solent. Nihil mali accidisse Scipioni puto, mihi accidit, si quid accidit; suis autem incommodis graviter angi non amicum sed se ipsum amantis est.

[11] Cum illo vero quis neget actum esse praeclare? Nisi enim, quod ille minime putabat, immortalitatem optare vellet, quid non adeptus est quod homini fas esset optare? qui summam spem civium, quam de eo iam puero habuerant, continuo adulescens incredibili virtute superavit, qui consulatum petivit numquam, factus consul est bis, primum ante tempus, iterum sibi suo tempore, rei publicae paene sero, qui duabus urbibus eversis inimicissimis huic imperio non modo praesentia verum etiam futura bella delevit. Quid dicam de moribus facillimis, de pietate in matrem, liberalitate in sorores, bonitate in suos, iustitia in omnes? nota sunt vobis. Quam autem civitati carus fuerit, maerore funeris indicatum est. Quid igitur hunc paucorum annorum accessio iuvare potuisset? Senectus enim quamvis non sit gravis, ut memini Catonem anno ante quam est mortuus mecum et cum Scipione disserere, tamen aufert eam viriditatem in qua etiam nunc erat Scipio.

[12] Quam ob rem vita quidem talis fuit vel fortuna vel gloria, ut nihil posset accedere, moriendi autem sensum celeritas abstulit; quo de genere mortis difficile dictu est; quid homines suspicentur, videtis; hoc vere tamen licet dicere, P. Scipioni ex multis diebus, quos in vita celeberrimos laetissimosque viderit, illum diem clarissimum fuisse, cum senatu dimisso domum reductus ad vesperum est a patribus conscriptis, populo Romano, sociis et Latinis, pridie quam excessit e vita, ut ex tam alto dignitatis gradu ad superos videatur deos potius quam ad inferos pervenisse.

[13] Neque enim assentior iis qui haec nuper disserere coeperunt, cum corporibus simul animos interire atque omnia morte deleri; plus apud me antiquorum auctoritas valet, vel nostrorum maiorum, qui mortuis tam religiosa iura tribuerunt, quod non fecissent profecto si nihil ad eos pertinere arbitrarentur, vel eorum qui in hac terra fuerunt magnamque Graeciam, quae nunc quidem deleta est, tum florebat, institutis et praeceptis suis erudierunt, vel eius qui Apollinis oraculo sapientissimus est iudicatus, qui non tum hoc, tum illud, ut in plerisque, sed idem semper, animos hominum esse divinos, iisque, cum ex corpore excessissent, reditum in caelum patere, optimoque et iustissimo cuique expeditissimum.

[14] Quod idem Scipioni videbatur, qui quidem, quasi praesagiret, perpaucis ante mortem diebus, cum et Philus et Manilius adesset et alii plures, tuque etiam, Scaevola, mecum venisses, triduum disseruit de re publica; cuius disputationis fuit extremum fere de immortalitate animorum, quae se in quiete per visum ex Africano audisse dicebat. Id si ita est, ut optimi cuiusque animus in morte facillime evolet tamquam e custodia vinclisque corporis, cui censemus cursum ad deos faciliorem fuisse quam Scipioni? Quocirca maerere hoc eius eventu vereor ne invidi magis quam amici sit. Sin autem illa veriora, ut idem interitus sit animorum et corporum nec ullus sensus maneat, ut nihil boni est in morte, sic certe nihil mali; sensu enim amisso fit idem, quasi natus non esset omnino, quem tamen esse natum et nos gaudemus et haec civitas dum erit laetabitur.

[15] Quam ob rem cum illo quidem, ut supra dixi, actum optime est, mecum incommodius, quem fuerat aequius, ut prius introieram, sic prius exire de vita. Sed tamen recordatione nostrae amicitiae sic fruor ut beate vixisse videar, quia cum Scipione vixerim, quocum mihi coniuncta cura de publica re et de privata fuit, quocum et domus fuit et militia communis et, id in quo est omnis vis amicitiae, voluntatum, studiorum, sententiarum summa consensio. Itaque non tam ista me sapientiae, quam modo Fannius commemoravit, fama delectat, falsa praesertim, quam quod amicitiae nostrae memoriam spero sempiternam fore, idque eo mihi magis est cordi, quod ex omnibus saeculis vix tria aut quattuor nominantur paria amicorum; quo in genere sperare videor Scipionis et Laeli amicitiam notam posteritati fore.

[16] Fannius: Istuc quidem, Laeli, ita necesse est. Sed quoniam amicitiae mentionem fecisti et sumus otiosi, pergratum mihi feceris, spero item Scaevolae, si quem ad modum soles de ceteris rebus, cum ex te quaeruntur, sic de amicitia disputaris quid sentias, qualem existimes, quae praecepta des.

Scaevola: Mihi vero erit gratum; atque id ipsum cum tecum agere conarer, Fannius antevertit. Quam ob rem utrique nostrum gratum admodum feceris.

[17] Laelius: Ego vero non gravarer, si mihi ipse confiderem; nam et praeclara res est et sumus, ut dixit Fannius, otiosi. Sed quis ego sum? aut quae est in me facultas? doctorum est ista consuetudo, eaque Graecorum, ut iis ponatur de quo disputent quamvis subito; magnum opus est egetque exercitatione non parva. Quam ob rem quae disputari de amicitia possunt, ab eis censeo petatis qui ista profitentur; ego vos hortari tantum possum ut amicitiam omnibus rebus humanis anteponatis; nihil est enim tam naturae aptum, tam conveniens ad res vel secundas vel adversas.

[18] Sed hoc primum sentio, nisi in bonis amicitiam esse non posse; neque id ad vivum reseco, ut illi qui haec subtilius disserunt, fortasse vere, sed ad communem utilitatem parum; negant enim quemquam esse virum bonum nisi sapientem. Sit ita sane; sed eam sapientiam interpretantur quam adhuc mortalis nemo est consecutus, nos autem ea quae sunt in usu vitaque communi, non ea quae finguntur aut optantur, spectare debemus. Numquam ego dicam C. Fabricium, M'. Curium, Ti. Coruncanium, quos sapientes nostri maiores iudicabant, ad istorum normam fuisse sapientes. Quare sibi habeant sapientiae nomen et invidiosum et obscurum; concedant ut viri boni fuerint. Ne id quidem facient, negabunt id nisi sapienti posse concedi.

[19] Agamus igitur pingui, ut aiunt, Minerva. Qui ita se gerunt, ita vivunt ut eorum probetur fides, integritas, aequitas, liberalitas, nec sit in eis ulla cupiditas, libido, audacia, sintque magna constantia, ut ii fuerunt modo quos nominavi, hos viros bonos, ut habiti sunt, sic etiam appellandos putemus, quia sequantur, quantum homines possunt, naturam optimam bene vivendi ducem. Sic enim mihi perspicere videor, ita natos esse nos ut inter omnes esset societas quaedam, maior autem ut quisque proxime accederet. Itaque cives potiores quam peregrini, propinqui quam alieni; cum his enim amicitiam natura ipsa peperit; sed ea non satis habet firmitatis. Namque hoc praestat amicitia propinquitati, quod ex propinquitate benevolentia tolli potest, ex amicitia non potest; sublata enim benevolentia amicitiae nomen tollitur, propinquitatis manet.

[20] Quanta autem vis amicitiae sit, ex hoc intellegi maxime potest, quod ex infinita societate generis humani, quam conciliavit ipsa natura, ita contracta res est et adducta in angustum ut omnis caritas aut inter duos aut inter paucos iungeretur.

Est enim amicitia nihil aliud nisi omnium divinarum humanarumque rerum cum benevolentia et caritate consensio; qua quidem haud scio an excepta sapientia nihil melius homini sit a dis immortalibus datum. Divitias alii praeponunt, bonam alii valetudinem, alii potentiam, alii honores, multi etiam voluptates. Beluarum hoc quidem extremum, illa autem superiora caduca et incerta, posita non tam in consiliis nostris quam in fortunae temeritate. Qui autem in virtute summum bonum ponunt, praeclare illi quidem, sed haec ipsa virtus amicitiam et gignit et continet nec sine virtute amicitia esse ullo pacto potest.

[21] Iam virtutem ex consuetudine vitae sermonisque nostri interpretemur nec eam, ut quidam docti, verborum magnificentia metiamur virosque bonos eos, qui habentur, numeremus, Paulos, Catones, Galos, Scipiones, Philos; his communis vita contenta est; eos autem omittamus, qui omnino nusquam reperiuntur.

[22] Talis igitur inter viros amicitia tantas opportunitates habet quantas vix queo dicere. Principio qui potest esse vita 'vitalis', ut ait Ennius, quae non in amici mutua benevolentia conquiescit? Quid dulcius quam habere quicum omnia audeas sic loqui ut tecum? Qui esset tantus fructus in prosperis rebus, nisi haberes, qui illis aeque ac tu ipse gauderet? adversas vero ferre difficile esset sine eo qui illas gravius etiam quam tu ferret. Denique ceterae res quae expetuntur opportunae sunt singulae rebus fere singulis, divitiae, ut utare, opes, ut colare, honores, ut laudere, voluptates, ut gaudeas, valetudo, ut dolore careas et muneribus fungare corporis; amicitia res plurimas continet; quoquo te verteris, praesto est, nullo loco excluditur, numquam intempestiva, numquam molesta est; itaque non aqua, non igni, ut aiunt, locis pluribus utimur quam amicitia. Neque ego nunc de vulgari aut de mediocri, quae tamen ipsa et delectat et prodest, sed de vera et perfecta loquor, qualis eorum qui pauci nominantur fuit. Nam et secundas res splendidiores facit amicitia et adversas partiens communicansque leviores.

[23] Cumque plurimas et maximas commoditates amicitia contineat, tum illa nimirum praestat omnibus, quod bonam spem praelucet in posterum nec debilitari animos aut cadere patitur. Verum enim amicum qui intuetur, tamquam exemplar aliquod intuetur sui. Quocirca et absentes adsunt et egentes abundant et imbecilli valent et, quod difficilius dictu est, mortui vivunt; tantus eos honos, memoria, desiderium prosequitur amicorum. Ex quo illorum beata mors videtur, horum vita laudabilis. Quod si exemeris ex rerum natura benevolentiae coniunctionem, nec domus ulla nec urbs stare poterit, ne agri quidem cultus permanebit. Id si minus intellegitur, quanta vis amicitiae concordiaeque sit, ex dissensionibus atque ex discordiis percipi potest. Quae enim domus tam stabilis, quae tam firma civitas est, quae non odiis et discidiis funditus possit everti? Ex quo quantum boni sit in amicitia iudicari potest.

[24] Agrigentinum quidem doctum quendam virum carminibus Graecis vaticinatum ferunt, quae in rerum natura totoque mundo constarent quaeque moverentur, ea contrahere amicitiam, dissipare discordiam. Atque hoc quidem omnes mortales et intellegunt et re probant. Itaque si quando aliquod officium exstitit amici in periculis aut adeundis aut communicandis, quis est qui id non maximis efferat laudibus? Qui clamores tota cavea nuper in hospitis et amici mei M. Pacuvi nova fabula! cum ignorante rege, uter Orestes esset, Pylades Orestem se esse diceret, ut pro illo necaretur, Orestes autem, ita ut erat, Orestem se esse perseveraret. Stantes plaudebant in re ficta; quid arbitramur in vera facturos fuisse? Facile indicabat ipsa natura vim suam, cum homines, quod facere ipsi non possent, id recte fieri in altero iudicarent.

Hactenus mihi videor de amicitia quid sentirem potuisse dicere; si quae praeterea sunt (credo autem esse multa), ab iis, si videbitur, qui ista disputant, quaeritote.

[25] Fannius: Nos autem a te potius; quamquam etiam ab istis saepe quaesivi et audivi non invitus equidem; sed aliud quoddam filum orationis tuae.

Scaevola: Tum magis id diceres, Fanni, si nuper in hortis Scipionis, cum est de re publica disputatum, adfuisses. Qualis tum patronus iustitiae fuit contra accuratam orationem Phili!

Fannius: Facile id quidem fuit iustitiam iustissimo viro defendere.

Scaevola: Quid? amicitiam nonne facile ei qui ob eam summa fide, constantia iustitiaque servatam maximam gloriam ceperit?

[26] Laelius: Vim hoc quidem est adferre. Quid enim refert qua me ratione cogatis? cogitis certe. Studiis enim generorum, praesertim in re bona, cum difficile est, tum ne aequum quidem obsistere.

Saepissime igitur mihi de amicitia cogitanti maxime illud considerandum videri solet, utrum propter imbecillitatem atque inopiam desiderata sit amicitia, ut dandis recipiendisque meritis quod quisque minus per se ipse posset, id acciperet ab alio vicissimque redderet, an esset hoc quidem proprium amicitiae, sed antiquior et pulchrior et magis a natura ipsa profecta alia causa. Amor enim, ex quo amicitia nominata est, princeps est ad benevolentiam coniungendam. Nam utilitates quidem etiam ab iis percipiuntur saepe qui simulatione amicitiae coluntur et observantur temporis causa, in amicitia autem nihil fictum est, nihil simulatum et, quidquid est, id est verum et voluntarium.

[27] Quapropter a natura mihi videtur potius quam ab indigentia orta amicitia, applicatione magis animi cum quodam sensu amandi quam cogitatione quantum illa res utilitatis esset habitura. Quod quidem quale sit, etiam in bestiis quibusdam animadverti potest, quae ex se natos ita amant ad quoddam tempus et ab eis ita amantur ut facile earum sensus appareat. Quod in homine multo est evidentius, primum ex ea caritate quae est inter natos et parentes, quae dirimi nisi detestabili scelere non potest; deinde cum similis sensus exstitit amoris, si aliquem nacti sumus cuius cum moribus et natura congruamus, quod in eo quasi lumen aliquod probitatis et virtutis perspicere videamur.

[28] Nihil est enim virtute amabilius, nihil quod magis adliciat ad diligendum, quippe cum propter virtutem et probitatem etiam eos, quos numquam vidimus, quodam modo diligamus. Quis est qui C. Fabrici, M'. Curi non cum caritate aliqua benevola memoriam usurpet, quos numquam viderit? quis autem est, qui Tarquinium Superbum, qui Sp. Cassium, Sp. Maelium non oderit? Cum duobus ducibus de imperio in Italia est decertatum, Pyrrho et Hannibale; ab altero propter probitatem eius non nimis alienos animos habemus, alterum propter crudelitatem semper haec civitas oderit.

[29] Quod si tanta vis probitatis est ut eam vel in iis quos numquam vidimus, vel, quod maius est, in hoste etiam diligamus, quid mirum est, si animi hominum moveantur, cum eorum, quibuscum usu coniuncti esse possunt, virtutem et bonitatem perspicere videantur? Quamquam confirmatur amor et beneficio accepto et studio perspecto et consuetudine adiuncta, quibus rebus ad illum primum motum animi et amoris adhibitis admirabilis quaedam exardescit benevolentiae magnitudo. Quam si qui putant ab imbecillitate proficisci, ut sit per quem adsequatur quod quisque desideret, humilem sane relinquunt et minime generosum, ut ita dicam, ortum amicitiae, quam ex inopia atque indigentia natam volunt. Quod si ita esset, ut quisque minimum esse in se arbitraretur, ita ad amicitiam esset aptissimus; quod longe secus est.

[30] Ut enim quisque sibi plurimum confidit et ut quisque maxime virtute et sapientia sic munitus est, ut nullo egeat suaque omnia in se ipso posita iudicet, ita in amicitiis expetendis colendisque maxime excellit. Quid enim? Africanus indigens mei? Minime hercule! ac ne ego quidem illius; sed ego admiratione quadam virtutis eius, ille vicissim opinione fortasse non nulla, quam de meis moribus habebat, me dilexit; auxit benevolentiam consuetudo. Sed quamquam utilitates multae et magnae consecutae sunt, non sunt tamen ab earum spe causae diligendi profectae.

[31] Ut enim benefici liberalesque sumus, non ut exigamus gratiam (neque enim beneficium faeneramur sed natura propensi ad liberalitatem sumus), sic amicitiam non spe mercedis adducti sed quod omnis eius fructus in ipso amore inest, expetendam putamus.

[32] Ab his qui pecudum ritu ad voluptatem omnia referunt longe dissentiunt, nec mirum; nihil enim altum, nihil magnificum ac divinum suspicere possunt qui suas omnes cogitationes abiecerunt in rem tam humilem tamque contemptam. Quam ob rem hos quidem ab hoc sermone removeamus, ipsi autem intellegamus natura gigni sensum diligendi et benevolentiae caritatem facta significatione probitatis. Quam qui adpetiverunt, applicant se et propius admovent ut et usu eius, quem diligere coeperunt, fruantur et moribus sintque pares in amore et aequales propensioresque ad bene merendum quam ad reposcendum, atque haec inter eos sit honesta certatio. Sic et utilitates ex amicitia maximae capientur et erit eius ortus a natura quam ab imbecillitate gravior et verior. Nam si utilitas amicitias conglutinaret, eadem commutata dissolveret; sed quia natura mutari non potest, idcirco verae amicitiae sempiternae sunt. Ortum quidem amicitiae videtis, nisi quid ad haec forte vultis.

Fannius: Tu vero perge, Laeli; pro hoc enim, qui minor est natu, meo iure respondeo.

[33] Scaevola: Recte tu quidem. Quam ob rem audiamus.

Laelius: Audite vero, optimi viri, ea quae saepissime inter me et Scipionem de amicitia disserebantur. Quamquam ille quidem nihil difficilius esse dicebat, quam amicitiam usque ad extremum vitae diem permanere. Nam vel ut non idem expediret, incidere saepe, vel ut de re publica non idem sentiretur; mutari etiam mores hominum saepe dicebat, alias adversis rebus, alias aetate ingravescente. Atque earum rerum exemplum ex similitudine capiebat ineuntis aetatis, quod summi puerorum amores saepe una cum praetexta toga ponerentur.

[34] Sin autem ad adulescentiam perduxissent, dirimi tamen interdum contentione vel uxoriae condicionis vel commodi alicuius, quod idem adipisci uterque non posset. Quod si qui longius in amicitia provecti essent, tamen saepe labefactari, si in honoris contentionem incidissent; pestem enim nullam maiorem esse amicitiis quam in plerisque pecuniae cupiditatem, in optimis quibusque honoris certamen et gloriae; ex quo inimicitias maximas saepe inter amicissimos exstitisse.

[35] Magna etiam discidia et plerumque iusta nasci, cum aliquid ab amicis quod rectum non esset postularetur, ut aut libidinis ministri aut adiutores essent ad iniuriam; quod qui recusarent, quamvis honeste id facerent, ius tamen amicitiae deserere arguerentur ab iis quibus obsequi nollent. Illos autem qui quidvis ab amico auderent postulare, postulatione ipsa profiteri omnia se amici causa esse facturos. Eorum querella inveterata non modo familiaritates exstingui solere sed odia etiam gigni sempiterna. Haec ita multa quasi fata impendere amicitiis ut omnia subterfugere non modo sapientiae sed etiam felicitatis diceret sibi videri.

[36] Quam ob rem id primum videamus, si placet, quatenus amor in amicitia progredi debeat. Numne, si Coriolanus habuit amicos, ferre contra patriam arma illi cum Coriolano debuerunt? num Vecellinum amici regnum adpetentem, num Maelium debuerunt iuvare?

[37] Ti. quidem Gracchum rem publicam vexantem a Q. Tuberone aequalibusque amicis derelictum videbamus. At C. Blossius Cumanus, hospes familiae vestrae, Scaevola, cum ad me, quod aderam Laenati et Rupilio consulibus in consilio, deprecatum venisset, hanc ut sibi ignoscerem, causam adferebat, quod tanti Ti. Gracchum fecisset ut, quidquid ille vellet, sibi faciendum putaret. Tum ego: 'Etiamne, si te in Capitolium faces ferre vellet?' 'Numquam' inquit 'voluisset id quidem; sed si voluisset, paruissem.' Videtis, quam nefaria vox! Et hercule ita fecit vel plus etiam quam dixit; non enim paruit ille Ti. Gracchi temeritati sed praefuit, nec se comitem illius furoris, sed ducem praebuit. Itaque hac amentia quaestione nova perterritus in Asiam profugit, ad hostes se contulit, poenas rei publicae graves iustasque persolvit. Nulla est igitur excusatio peccati, si amici causa peccaveris; nam cum conciliatrix amicitiae virtutis opinio fuerit, difficile est amicitiam manere, si a virtute defeceris.

[38] Quod si rectum statuerimus vel concedere amicis, quidquid velint, vel impetrare ab iis, quidquid velimus, perfecta quidem sapientia si simus, nihil habeat res vitii; sed loquimur de iis amicis qui ante oculos sunt, quos vidimus aut de quibus memoriam accepimus, quos novit vita communis. Ex hoc numero nobis exempla sumenda sunt, et eorum quidem maxime qui ad sapientiam proxime accedunt.

[39] Videmus Papum Aemilium Luscino familiarem fuisse (sic a patribus accepimus), bis una consules, collegas in censura; tum et cum iis et inter se coniunctissimos fuisse M'. Curium, Ti. Coruncanium memoriae proditum est. Igitur ne suspicari quidem possumus quemquam horum ab amico quippiam contendisse, quod contra fidem, contra ius iurandum, contra rem publicam esset. Nam hoc quidem in talibus viris quid attinet dicere, si contendisset, impetraturum non fuisse? cum illi sanctissimi viri fuerint, aeque autem nefas sit tale aliquid et facere rogatum et rogare. At vero Ti. Gracchum sequebantur C. Carbo, C. Cato, et minime tum quidem C. frater, nunc idem acerrimus.

[40] Haec igitur lex in amicitia sanciatur, ut neque rogemus res turpes nec faciamus rogati. Turpis enim excusatio est et minime accipienda cum in ceteris peccatis, tum si quis contra rem publicam se amici causa fecisse fateatur. Etenim eo loco, Fanni et Scaevola, locati sumus ut nos longe prospicere oporteat futuros casus rei publicae. Deflexit iam aliquantum de spatio curriculoque consuetudo maiorum.

[41] Ti. Gracchus regnum occupare conatus est, vel regnavit is quidem paucos menses. Num quid simile populus Romanus audierat aut viderat? Hunc etiam post mortem secuti amici et propinqui quid in P. Scipione effecerint, sine lacrimis non queo dicere. Nam Carbonem, quocumque modo potuimus, propter recentem poenam Ti. Gracchi sustinuimus; de C. Gracchi autem tribunatu quid expectem, non libet augurari. Serpit deinde res; quae proclivis ad perniciem, cum semel coepit, labitur. Videtis in tabella iam ante quanta sit facta labes, primo Gabinia lege, biennio autem post Cassia. Videre iam videor populum a senatu disiunctum, multitudinis arbitrio res maximas agi. Plures enim discent quem ad modum haec fiant, quam quem ad modum iis resistatur.

[42] Quorsum haec? Quia sine sociis nemo quicquam tale conatur. Praecipiendum est igitur bonis ut, si in eius modi amicitias ignari casu aliquo inciderint, ne existiment ita se alligatos ut ab amicis in magna aliqua re publica peccantibus non discedant; improbis autem poena statuenda est, nec vero minor iis qui secuti erunt alterum, quam iis qui ipsi fuerint impietatis duces. Quis clarior in Graecia Themistocle, quis potentior? qui cum imperator bello Persico servitute Graeciam liberavisset propterque invidiam in exsilium expulsus esset, ingratae patriae iniuriam non tulit, quam ferre debuit, fecit idem, quod xx annis ante apud nos fecerat Coriolanus. His adiutor contra patriam inventus est nemo; itaque mortem sibi uterque conscivit.

[43] Quare talis improborum consensio non modo excusatione amicitiae tegenda non est sed potius supplicio omni vindicanda est, ut ne quis concessum putet amicum vel bellum patriae inferentem sequi; quod quidem, ut res ire coepit, haud scio an aliquando futurum sit. Mihi autem non minori curae est, qualis res publica post mortem meam futura, quam qualis hodie sit.

[44] Haec igitur prima lex amicitiae sanciatur, ut ab amicis honesta petamus, amicorum causa honesta faciamus, ne exspectemus quidem, dum rogemur; studium semper adsit, cunctatio absit; consilium vero dare audeamus libere. Plurimum in amicitia amicorum bene suadentium valeat auctoritas, eaque et adhibeatur ad monendum non modo aperte sed etiam acriter, si res postulabit, et adhibitae pareatur.

[45] Nam quibusdam, quos audio sapientes habitos in Graecia, placuisse opinor mirabilia quaedam (sed nihil est quod illi non persequantur argutiis): partim fugiendas esse nimias amicitias, ne necesse sit unum sollicitum esse pro pluribus; satis superque esse sibi suarum cuique rerum, alienis nimis implicari molestum esse; commodissimum esse quam laxissimas habenas habere amicitiae, quas vel adducas, cum velis, vel remittas; caput enim esse ad beate vivendum securitatem, qua frui non possit animus, si tamquam parturiat unus pro pluribus.

[46] Alios autem dicere aiunt multo etiam inhumanius (quem locum breviter paulo ante perstrinxi) praesidii adiumentique causa, non benevolentiae neque caritatis, amicitias esse expetendas; itaque, ut quisque minimum firmitatis haberet minimumque virium, ita amicitias appetere maxime; ex eo fieri ut mulierculae magis amicitiarum praesidia quaerant quam viri et inopes quam opulenti et calamitosi quam ii qui putentur beati.

[47] O praeclaram sapientiam! Solem enim e mundo tollere videntur, qui amicitiam e vita tollunt, qua nihil a dis immortalibus melius habemus, nihil iucundius. Quae est enim ista securitas? Specie quidem blanda sed reapse multis locis repudianda. Neque enim est consentaneum ullam honestam rem actionemve, ne sollicitus sis, aut non suscipere aut susceptam deponere. Quod si curam fugimus, virtus fugienda est, quae necesse est cum aliqua cura res sibi contrarias aspernetur atque oderit, ut bonitas malitiam, temperantia libidinem, ignaviam fortitudo; itaque videas rebus iniustis iustos maxime dolere, imbellibus fortes, flagitiosis modestos. Ergo hoc proprium est animi bene constituti, et laetari bonis rebus et dolere contrariis.

[48] Quam ob rem si cadit in sapientem animi dolor, qui profecto cadit, nisi ex eius animo exstirpatam humanitatem arbitramur, quae causa est cur amicitiam funditus tollamus e vita, ne aliquas propter eam suscipiamus molestias? Quid enim interest motu animi sublato non dico inter pecudem et hominem, sed inter hominem et truncum aut saxum aut quidvis generis eiusdem? Neque enim sunt isti audiendi qui virtutem duram et quasi ferream esse quandam volunt; quae quidem est cum multis in rebus, tum in amicitia tenera atque tractabilis, ut et bonis amici quasi diffundatur et incommodis contrahatur. Quam ob rem angor iste, qui pro amico saepe capiendus est, non tantum valet ut tollat e vita amicitiam, non plus quam ut virtutes, quia non nullas curas et molestias adferunt, repudientur.

Cum autem contrahat amicitiam, ut supra dixi, si qua significatio virtutis eluceat, ad quam se similis animus applicet et adiungat, id cum contigit, amor exoriatur necesse est.

[49] Quid enim tam absurdum quam delectari multis inanimis rebus, ut honore, ut gloria, ut aedificio, ut vestitu cultuque corporis, animante virtute praedito, eo qui vel amare vel, ut ita dicam, redamare possit, non admodum delectari? Nihil est enim remuneratione benevolentiae, nihil vicissitudine studiorum officiorumque iucundius.

[50] Quid, si illud etiam addimus, quod recte addi potest, nihil esse quod ad se rem ullam tam alliciat et attrahat quam ad amicitiam similitudo? concedetur profecto verum esse, ut bonos boni diligant adsciscantque sibi quasi propinquitate coniunctos atque natura. Nihil est enim appetentius similium sui nec rapacius quam natura. Quam ob rem hoc quidem, Fanni et Scaevola, constet, ut opinor, bonis inter bonos quasi necessariam benevolentiam, qui est amicitiae fons a natura constitutus. Sed eadem bonitas etiam ad multitudinem pertinet. Non enim est inhumana virtus neque immunis neque superba, quae etiam populos universos tueri iisque optime consulere soleat; quod non faceret profecto, si a caritate vulgi abhorreret.

[51] Atque etiam mihi quidem videntur, qui utilitatum causa fingunt amicitias, amabilissimum nodum amicitiae tollere. Non enim tam utilitas parta per amicum quam amici amor ipse delectat, tumque illud fit, quod ab amico est profectum, iucundum, si cum studio est profectum; tantumque abest, ut amicitiae propter indigentiam colantur, ut ii qui opibus et copiis maximeque virtute, in qua plurimum est praesidii, minime alterius indigeant, liberalissimi sint et beneficentissimi. Atque haud sciam an ne opus sit quidem nihil umquam omnino deesse amicis. Ubi enim studia nostra viguissent, si numquam consilio, numquam opera nostra nec domi nec militiae Scipio eguisset? Non igitur utilitatem amicitia, sed utilitas amicitiam secuta est.

[52] Non ergo erunt homines deliciis diffluentes audiendi, si quando de amicitia, quam nec usu nec ratione habent cognitam, disputabunt. Nam quis est, pro deorum fidem atque hominum! qui velit, ut neque diligat quemquam nec ipse ab ullo diligatur, circumfluere omnibus copiis atque in omnium rerum abundantia vivere? Haec enim est tyrannorum vita nimirum, in qua nulla fides, nulla caritas, nulla stabilis benevolentiae potest esse fiducia, omnia semper suspecta atque sollicita, nullus locus amicitiae.

[53] Quis enim aut eum diligat quem metuat, aut eum a quo se metui putet? Coluntur tamen simulatione dumtaxat ad tempus. Quod si forte, ut fit plerumque, ceciderunt, tum intellegitur quam fuerint inopes amicorum. Quod Tarquinium dixisse ferunt, tum exsulantem se intellexisse quos fidos amicos habuisset, quos infidos, cum iam neutris gratiam referre posset.

[54] Quamquam miror, illa superbia et importunitate si quemquam amicum habere potuit. Atque ut huius, quem dixi, mores veros amicos parare non potuerunt, sic multorum opes praepotentium excludunt amicitias fideles. Non enim solum ipsa Fortuna caeca est sed eos etiam plerumque efficit caecos quos complexa est; itaque efferuntur fere fastidio et contumacia nec quicquam insipiente fortunato intolerabilius fieri potest. Atque hoc quidem videre licet, eos qui antea commodis fuerint moribus, imperio, potestate, prosperis rebus immutari, sperni ab iis veteres amicitias, indulgeri novis.

[55] Quid autem stultius quam, cum plurimum copiis, facultatibus, opibus possint, cetera parare, quae parantur pecunia, equos, famulos, vestem egregiam, vasa pretiosa, amicos non parare, optimam et pulcherrimam vitae, ut ita dicam, supellectilem? etenim cetera cum parant, cui parent, nesciunt, nec cuius causa laborent (eius enim est istorum quidque, qui vicit viribus), amicitiarum sua cuique permanet stabilis et certa possessio; ut, etiamsi illa maneant, quae sunt quasi dona Fortunae, tamen vita inculta et deserta ab amicis non possit esse iucunda. Sed haec hactenus.

[56] Constituendi autem sunt qui sint in amicitia fines et quasi termini diligendi. De quibus tres video sententias ferri, quarum nullam probo, unam, ut eodem modo erga amicum adfecti simus, quo erga nosmet ipsos, alteram, ut nostra in amicos benevolentia illorum erga nos benevolentiae pariter aequaliterque respondeat, tertiam, ut, quanti quisque se ipse facit, tanti fiat ab amicis.

[57] Harum trium sententiarum nulli prorsus assentior. Nec enim illa prima vera est, ut, quem ad modum in se quisque sit, sic in amicum sit animatus. Quam multa enim, quae nostra causa numquam faceremus, facimus causa amicorum! precari ab indigno, supplicare, tum acerbius in aliquem invehi insectarique vehementius, quae in nostris rebus non satis honeste, in amicorum fiunt honestissime; multaeque res sunt in quibus de suis commodis viri boni multa detrahunt detrahique patiuntur, ut iis amici potius quam ipsi fruantur.

[58] Altera sententia est, quae definit amicitiam paribus officiis ac voluntatibus. Hoc quidem est nimis exigue et exiliter ad calculos vocare amicitiam, ut par sit ratio acceptorum et datorum. Divitior mihi et affluentior videtur esse vera amicitia nec observare restricte, ne plus reddat quam acceperit; neque enim verendum est, ne quid excidat, aut ne quid in terram defluat, aut ne plus aequo quid in amicitiam congeratur.

[59] Tertius vero ille finis deterrimus, ut, quanti quisque se ipse faciat, tanti fiat ab amicis. Saepe enim in quibusdam aut animus abiectior est aut spes amplificandae fortunae fractior. Non est igitur amici talem esse in eum qualis ille in se est, sed potius eniti et efficere ut amici iacentem animum excitet inducatque in spem cogitationemque meliorem. Alius igitur finis verae amicitiae constituendus est, si prius, quid maxime reprehendere Scipio solitus sit, dixero. Negabat ullam vocem inimiciorem amicitiae potuisse reperiri quam eius, qui dixisset ita amare oportere, ut si aliquando esset osurus; nec vero se adduci posse, ut hoc, quem ad modum putaretur, a Biante esse dictum crederet, qui sapiens habitus esset unus e septem; impuri cuiusdam aut ambitiosi aut omnia ad suam potentiam revocantis esse sententiam. Quonam enim modo quisquam amicus esse poterit ei, cui se putabit inimicum esse posse? quin etiam necesse erit cupere et optare, ut quam saepissime peccet amicus, quo plures det sibi tamquam ansas ad reprehendendum; rursum autem recte factis commodisque amicorum necesse erit angi, dolere, invidere.

[60] Quare hoc quidem praeceptum, cuiuscumque est, ad tollendam amicitiam valet; illud potius praecipiendum fuit, ut eam diligentiam adhiberemus in amicitiis comparandis, ut ne quando amare inciperemus eum, quem aliquando odisse possemus. Quin etiam si minus felices in diligendo fuissemus, ferendum id Scipio potius quam inimicitiarum tempus cogitandum putabat.

[61] His igitur finibus utendum arbitror, ut, cum emendati mores amicorum sint, tum sit inter eos omnium rerum, consiliorum, voluntatum sine ulla exceptione communitas, ut, etiamsi qua fortuna acciderit ut minus iustae amicorum voluntates adiuvandae sint, in quibus eorum aut caput agatur aut fama, declinandum de via sit, modo ne summa turpitudo sequatur; est enim quatenus amicitiae dari venia possit. Nec vero neglegenda est fama nec mediocre telum ad res gerendas existimare oportet benevolentiam civium; quam blanditiis et assentando colligere turpe est; virtus, quam sequitur caritas, minime repudianda est.

[62] Sed (saepe enim redeo ad Scipionem, cuius omnis sermo erat de amicitia) querebatur, quod omnibus in rebus homines diligentiores essent; capras et oves quot quisque haberet, dicere posse, amicos quot haberet, non posse dicere et in illis quidem parandis adhibere curam, in amicis eligendis neglegentis esse nec habere quasi signa quaedam et notas, quibus eos qui ad amicitias essent idonei, iudicarent. Sunt igitur firmi et stabiles et constantes eligendi; cuius generis est magna penuria. Et iudicare difficile est sane nisi expertum; experiendum autem est in ipsa amicitia. Ita praecurrit amicitia iudicium tollitque experiendi potestatem.

[63] Est igitur prudentis sustinere ut cursum, sic impetum benevolentiae, quo utamur quasi equis temptatis, sic amicitia ex aliqua parte periclitatis moribus amicorum. Quidam saepe in parva pecunia perspiciuntur quam sint leves, quidam autem, quos parva movere non potuit, cognoscuntur in magna. Sin vero erunt aliqui reperti qui pecuniam praeferre amicitiae sordidum existiment, ubi eos inveniemus, qui honores, magistratus, imperia, potestates, opes amicitiae non anteponant, ut, cum ex altera parte proposita haec sint, ex altera ius amicitiae, non multo illa malint? Imbecilla enim est natura ad contemnendam potentiam; quam etiamsi neglecta amicitia consecuti sint, obscuratum iri arbitrantur, quia non sine magna causa sit neglecta amicitia.

[64] Itaque verae amicitiae difficillime reperiuntur in iis qui in honoribus reque publica versantur; ubi enim istum invenias qui honorem amici anteponat suo? Quid? haec ut omittam, quam graves, quam difficiles plerisque videntur calamitatuam societates! ad quas non est facile inventu qui descendant. Quamquam Ennius recte:

Amicus certus in re incerta cernitur,

tamen haec duo levitatis et infirmitatis plerosque convincunt, aut si in bonis rebus contemnunt aut in malis deserunt. Qui igitur utraque in re gravem, constantem, stabilem se in amicitia praestiterit, hunc ex maxime raro genere hominum iudicare debemus et paene divino.

[65] Firmamentum autem stabilitatis constantiaeque eius, quam in amicitia quaerimus, fides est; nihil est enim stabile quod infidum est. Simplicem praeterea et communem et consentientem, id est qui rebus isdem moveatur, eligi par est, quae omnia pertinent ad fidelitatem; neque enim fidum potest esse multiplex ingenium et tortuosum, neque vero, qui non isdem rebus movetur naturaque consentit, aut fidus aut stabilis potest esse. Addendum eodem est, ut ne criminibus aut inferendis delectetur aut credat oblatis, quae pertinent omnia ad eam, quam iam dudum tracto, constantiam. Ita fit verum illud, quod initio dixi, amicitiam nisi inter bonos esse non posse. Est enim boni viri, quem eundem sapientem licet dicere, haec duo tenere in amicitia: primum ne quid fictum sit neve simulatum; aperte enim vel odisse magis ingenui est quam fronte occultare sententiam; deinde non solum ab aliquo allatas criminationes repellere, sed ne ipsum quidem esse suspiciosum, semper aliquid existimantem ab amico esse violatum.

[66] Accedat huc suavitas quaedam oportet sermonum atque morum, haudquaquam mediocre condimentum amicitiae. Tristitia autem et in omni re severitas habet illa quidem gravitatem, sed amicitia remissior esse debet et liberior et dulcior et ad omnem comitatem facilitatemque proclivior.

[67] Exsistit autem hoc loco quaedam quaestio subdifficilis, num quando amici novi, digni amicitia, veteribus sint anteponendi, ut equis vetulis teneros anteponere solemus. Indigna homine dubitatio! Non enim debent esse amicitiarum sicut aliarum rerum satietates; veterrima quaeque, ut ea vina, quae vetustatem ferunt, esse debet suavissima; verumque illud est, quod dicitur, multos modios salis simul edendos esse, ut amicitiae munus expletum sit.

[68] Novitates autem si spem adferunt, ut tamquam in herbis non fallacibus fructus appareat, non sunt illae quidem repudiandae, vetustas tamen suo loco conservanda; maxima est enim vis vetustatis et consuetudinis. Quin in ipso equo, cuius modo feci mentionem, si nulla res impediat, nemo est, quin eo, quo consuevit, libentius utatur quam intractato et novo. Nec vero in hoc quod est animal, sed in iis etiam quae sunt inanima, consuetudo valet, cum locis ipsis delectemur, montuosis etiam et silvestribus, in quibus diutius commorati sumus.

[69] Sed maximum est in amicitia parem esse inferiori. Saepe enim excellentiae quaedam sunt, qualis erat Scipionis in nostro, ut ita dicam, grege. Numquam se ille Philo, numquam Rupilio, numquam Mummio anteposuit, numquam inferioris ordinis amicis, Q. vero Maximum fratrem, egregium virum omnino, sibi nequaquam parem, quod is anteibat aetate, tamquam superiorem colebat suosque omnes per se posse esse ampliores volebat.

[70] Quod faciendum imitandumque est omnibus, ut, si quam praestantiam virtutis, ingenii, fortunae consecuti sint, impertiant ea suis communicentque cum proximis, ut, si parentibus nati sint humilibus, si propinquos habeant imbecilliore vel animo vel fortuna, eorum augeant opes eisque honori sint et dignitati. Ut in fabulis, qui aliquamdiu propter ignorationem stirpis et generis in famulatu fuerunt, cum cogniti sunt et aut deorum aut regum filii inventi, retinent tamen caritatem in pastores, quos patres multos annos esse duxerunt. Quod est multo profecto magis in veris patribus certisque faciendum. Fructus enim ingenii et virtutis omnisque praestantiae tum maximus capitur, cum in proximum quemque confertur.

[71] Ut igitur ii qui sunt in amicitiae coniunctionisque necessitudine superiores, exaequare se cum inferioribus debent, sic inferiores non dolere se a suis aut ingenio aut fortuna aut dignitate superari. Quorum plerique aut queruntur semper aliquid aut etiam exprobrant, eoque magis, si habere se putant, quod officiose et amice et cum labore aliquo suo factum queant dicere. Odiosum sane genus hominum officia exprobrantium; quae meminisse debet is in quem conlata sunt, non commemorare, qui contulit.

[72] Quam ob rem ut ii qui superiores sunt submittere se debent in amicitia, sic quodam modo inferiores extollere. Sunt enim quidam qui molestas amicitias faciunt, cum ipsi se contemni putant; quod non fere contingit nisi iis qui etiam contemnendos se arbitrantur; qui hac opinione non modo verbis sed etiam opere levandi sunt.

[73] Tantum autem cuique tribuendum, primum quantum ipse efficere possis, deinde etiam quantum ille quem diligas atque adiuves, sustinere. Non enim neque tu possis, quamvis excellas, omnes tuos ad honores amplissimos perducere, ut Scipio P. Rupilium potuit consulem efficere, fratrem eius L. non potuit. Quod si etiam possis quidvis deferre ad alterum, videndum est tamen, quid ille possit sustinere.

[74] Omnino amicitiae corroboratis iam confirmatisque et ingeniis et aetatibus iudicandae sunt, nec si qui ineunte aetate venandi aut pilae studiosi fuerunt, eos habere necessarios quos tum eodem studio praeditos dilexerunt. Isto enim modo nutrices et paedagogi iure vetustatis plurimum benevolentiae postulabunt; qui neglegendi quidem non sunt sed alio quodam modo aestimandi. Aliter amicitiae stabiles permanere non possunt. Dispares enim mores disparia studia sequuntur, quorum dissimilitudo dissociat amicitias; nec ob aliam causam ullam boni improbis, improbi bonis amici esse non possunt, nisi quod tanta est inter eos, quanta maxima potest esse, morum studiorumque distantia.

[75] Recte etiam praecipi potest in amicitiis, ne intemperata quaedam benevolentia, quod persaepe fit, impediat magnas utilitates amicorum. Nec enim, ut ad fabulas redeam, Troiam Neoptolemus capere potuisset, si Lycomedem, apud quem erat educatus, multis cum lacrimis iter suum impedientem audire voluisset. Et saepe incidunt magnae res, ut discedendum sit ab amicis; quas qui impedire vult, quod desiderium non facile ferat, is et infirmus est mollisque natura et ob eam ipsam causam in amicitia parum iustus.

[76] Atque in omni re considerandum est et quid postules ab amico et quid patiare a te impetrari.

Est etiam quaedam calamitas in amicitiis dimittendis non numquam necessaria; iam enim a sapientium familiaritatibus ad vulgares amicitias oratio nostra delabitur. Erumpunt saepe vitia amicorum tum in ipsos amicos, tum in alienos, quorum tamen ad amicos redundet infamia. Tales igitur amicitiae sunt remissione usus eluendae et, ut Catonem dicere audivi, dissuendae magis quam discindendae, nisi quaedam admodum intolerabilis iniuria exarserit, ut neque rectum neque honestum sit nec fieri possit, ut non statim alienatio disiunctioque faciunda sit.

[77] Sin autem aut morum aut studiorum commutatio quaedam, ut fieri solet, facta erit aut in rei publicae partibus dissensio intercesserit (loquor enim iam, ut paulo ante dixi, non de sapientium sed de communibus amicitiis), cavendum erit, ne non solum amicitiae depositae, sed etiam inimicitiae susceptae videantur. Nihil est enim turpius quam cum eo bellum gerere quocum familiariter vixeris. Ab amicitia Q. Pompei meo nomine se removerat, ut scitis, Scipio; propter dissensionem autem, quae erat in re publica, alienatus est a collega nostro Metello; utrumque egit graviter, auctoritate et offensione animi non acerba.

[78] Quam ob rem primum danda opera est ne qua amicorum discidia fiant; sin tale aliquid evenerit, ut exstinctae potius amicitiae quam oppressae videantur. Cavendum vero ne etiam in graves inimicitias convertant se amicitiae; ex quibus iurgia, maledicta, contumeliae gignuntur. Quae tamen si tolerabiles erunt, ferendae sunt, et hic honos veteri amicitiae tribuendus, ut is in culpa sit qui faciat, non is qui patiatur iniuriam.

Omnino omnium horum vitiorum atque incommodorum una cautio est atque una provisio, ut ne nimis cito diligere incipiant neve non dignos.

[79] Digni autem sunt amicitia quibus in ipsis inest causa cur diligantur. Rarum genus. Et quidem omnia praeclara rara, nec quicquam difficilius quam reperire quod sit omni ex parte in suo genere perfectum. Sed plerique neque in rebus humanis quicquam bonum norunt, nisi quod fructuosum sit, et amicos tamquam pecudes eos potissimum diligunt ex quibus sperant se maximum fructum esse capturos.

[80] Ita pulcherrima illa et maxime naturali carent amicitia per se et propter se expetita nec ipsi sibi exemplo sunt, haec vis amicitiae et qualis et quanta sit. Ipse enim se quisque diligit, non ut aliquam a se ipse mercedem exigat caritatis suae, sed quod per se sibi quisque carus est. Quod nisi idem in amicitiam transferetur, verus amicus numquam reperietur; est enim is qui est tamquam alter idem.

[81] Quod si hoc apparet in bestiis, volucribus, nantibus, agrestibus, cicuribus, feris, primum ut se ipsae diligant (id enim pariter cum omni animante nascitur), deinde ut requirant atque appetant ad quas se applicent eiusdem generis animantis, idque faciunt cum desiderio et cum quadam similitudine amoris humani, quanto id magis in homine fit natura! qui et se ipse diligit et alterum anquirit, cuius animum ita cum suo misceat ut efficiat paene unum ex duobus.

[82] Sed plerique perverse, ne dicam impudenter, habere talem amicum volunt, quales ipsi esse non possunt, quaeque ipsi non tribuunt amicis, haec ab iis desiderant. Par est autem primum ipsum esse virum bonum, tum alterum similem sui quaerere. In talibus ea, quam iam dudum tractamus, stabilitas amicitiae confirmari potest, cum homines benevolentia coniuncti primum cupiditatibus iis quibus ceteri serviunt imperabunt, deinde aequitate iustitiaque gaudebunt, omniaque alter pro altero suscipiet, neque quicquam umquam nisi honestum et rectum alter ab altero postulabit, neque solum colent inter se ac diligent sed etiam verebuntur. Nam maximum ornamentum amicitiae tollit qui ex ea tollit verecundiam.

[83] Itaque in iis perniciosus est error qui existimant libidinum peccatorumque omnium patere in amicitia licentiam; virtutum amicitia adiutrix a natura data est, non vitiorum comes, ut, quoniam solitaria non posset virtus ad ea, quae summa sunt, pervenire, coniuncta et consociata cum altera perveniret. Quae si quos inter societas aut est aut fuit aut futura est, eorum est habendus ad summum naturae bonum optumus beatissimusque comitatus.

[84] Haec est, inquam, societas, in qua omnia insunt, quae putant homines expetenda, honestas, gloria, tranquillitas animi atque iucunditas, ut et, cum haec adsint, beata vita sit et sine his esse non possit. Quod cum optimum maximumque sit, si id volumus adipisci, virtuti opera danda est, sine qua nec amicitiam neque ullam rem expetendam consequi possumus; ea vero neglecta qui se amicos habere arbitrantur, tum se denique errasse sentiunt, cum eos gravis aliquis casus experiri cogit.

[85] Quocirca (dicendum est enim saepius), cum iudicaris, diligere oportet, non, cum dilexeris, iudicare. Sed cum multis in rebus neglegentia plectimur, tum maxime in amicis et diligendis et colendis; praeposteris enim utimur consiliis et acta agimus, quod vetamur vetere proverbio. Nam implicati ultro et citro vel usu diuturno vel etiam officiis repente in medio cursu amicitias exorta aliqua offensione disrumpimus.

[86] Quo etiam magis vituperanda est rei maxime necessariae tanta incuria. Una est enim amicitia in rebus humanis, de cuius utilitate omnes uno ore consentiunt. Quamquam a multis virtus ipsa contemnitur et venditatio quaedam atque ostentatio esse dicitur; multi divitias despiciunt, quos parvo contentos tenuis victus cultusque delectat; honores vero, quorum cupiditate quidam inflammantur, quam multi ita contemnunt, ut nihil inanius, nihil esse levius existiment! itemque cetera, quae quibusdam admirabilia videntur, permulti sunt qui pro nihilo putent; de amicitia omnes ad unum idem sentiunt, et ii qui ad rem publicam se contulerunt, et ii qui rerum cognitione doctrinaque delectantur, et ii qui suum negotium gerunt otiosi, postremo ii qui se totos tradiderunt voluptatibus, sine amicitia vitam esse nullam, si modo velint aliqua ex parte liberaliter vivere.

[87] Serpit enim nescio quo modo per omnium vitas amicitia nec ullam aetatis degendae rationem patitur esse expertem sui. Quin etiam si quis asperitate ea est et immanitate naturae, congressus ut hominum fugiat atque oderit, qualem fuisse Athenis Timonem nescio quem accepimus, tamen is pati non possit, ut non anquirat aliquem, apud quem evomat virus acerbitatis suae. Atque hoc maxime iudicaretur, si quid tale posset contingere, ut aliquis nos deus ex hac hominum frequentia tolleret et in solitudine uspiam collocaret atque ibi suppeditans omnium rerum, quas natura desiderat, abundantiam et copiam hominis omnino aspiciendi potestatem eriperet. Quis tam esset ferreus qui eam vitam ferre posset, cuique non auferret fructum voluptatum omnium solitudo?

[88] Verum ergo illud est quod a Tarentino Archyta, ut opinor, dici solitum nostros senes commemorare audivi ab aliis senibus auditum: 'si quis in caelum ascendisset naturamque mundi et pulchritudinem siderum perspexisset, insuavem illam admirationem ei fore; quae iucundissima fuisset, si aliquem, cui narraret, habuisset.' Sic natura solitarium nihil amat semperque ad aliquod tamquam adminiculum adnititur; quod in amicissimo quoque dulcissimum est.

Sed cum tot signis eadem natura declaret, quid velit, anquirat, desideret, tamen obsurdescimus nescio quo modo nec ea, quae ab ea monemur, audimus. Est enim varius et multiplex usus amicitiae, multaeque causae suspicionum offensionumque dantur, quas tum evitare, tum elevare, tum ferre sapientis est; una illa sublevanda offensio est, ut et utilitas in amicitia et fides retineatur: nam et monendi amici saepe sunt et obiurgandi, et haec accipienda amice, cum benevole fiunt.

[89] Sed nescio quo modo verum est, quod in Andria familiaris meus dicit:

Obsequium amicos, veritas odium parit.

Molesta veritas, siquidem ex ea nascitur odium, quod est venenum amicitiae, sed obsequium multo molestius, quod peccatis indulgens praecipitem amicum ferri sinit; maxima autem culpa in eo, qui et veritatem aspernatur et in fraudem obsequio impellitur. Omni igitur hac in re habenda ratio et diligentia est, primum ut monitio acerbitate, deinde ut obiurgatio contumelia careat; in obsequio autem, quoniam Terentiano verbo libenter utimur, comitas adsit, assentatio, vitiorum adiutrix, procul amoveatur, quae non modo amico, sed ne libero quidem digna est; aliter enim cum tyranno, aliter cum amico vivitur.

[90] Cuius autem aures clausae veritati sunt, ut ab amico verum audire nequeat, huius salus desperanda est. Scitum est enim illud Catonis, ut multa: 'melius de quibusdam acerbos inimicos mereri quam eos amicos qui dulces videantur; illos verum saepe dicere, hos numquam.' Atque illud absurdum, quod ii, qui monentur, eam molestiam quam debent capere non capiunt, eam capiunt qua debent vacare; peccasse enim se non anguntur, obiurgari moleste ferunt; quod contra oportebat, delicto dolere, correctione gaudere.

[91] Ut igitur et monere et moneri proprium est verae amicitiae et alterum libere facere, non aspere, alterum patienter accipere, non repugnanter, sic habendum est nullam in amicitiis pestem esse maiorem quam adulationem, blanditiam, assentationem; quamvis enim multis nominibus est hoc vitium notandum levium hominum atque fallacium ad voluntatem loquentium omnia, nihil ad veritatem.

[92] Cum autem omnium rerum simulatio vitiosa est (tollit enim iudicium veri idque adulterat), tum amicitiae repugnat maxime; delet enim veritatem, sine qua nomen amicitiae valere non potest. Nam cum amicitiae vis sit in eo, ut unus quasi animus fiat ex pluribus, qui id fieri poterit, si ne in uno quidem quoque unus animus erit idemque semper, sed varius, commutabilis, multiplex?

[93] Quid enim potest esse tam flexibile, tam devium quam animus eius qui ad alterius non modo sensum ac voluntatem sed etiam vultum atque nutum convertitur?

Negat quis, nego; ait, aio; postremo imperavi egomet mihi 
Omnia adsentari,

ut ait idem Terentius, sed ille in Gnathonis persona, quod amici genus adhibere omnino levitatis est.

[94] Multi autem Gnathonum similes cum sint loco, fortuna, fama superiores, horum est assentatio molesta, cum ad vanitatem accessit auctoritas.

[95] Secerni autem blandus amicus a vero et internosci tam potest adhibita diligentia quam omnia fucata et simulata a sinceris atque veris. Contio, quae ex imperitissimis constat, tamen iudicare solet quid intersit inter popularem, id est assentatorem et levem civem, et inter constantem et severum et gravem.

[96] Quibus blanditiis C. Papirius nuper influebat in auris contionis, cum ferret legem de tribunis plebis reficiendis! Dissuasimus nos; sed nihil de me, de Scipione dicam libentius. Quanta illi, di immortales, fuit gravitas, quanta in oratione maiestas! ut facile ducem populi Romani, non comitem diceres. Sed adfuistis, et est in manibus oratio. Itaque lex popularis suffragiis populi repudiata est. Atque, ut ad me redeam, meministis, Q. Maximo, fratre Scipionis, et L. Mancino consulibus, quam popularis lex de sacerdotiis C. Licini Crassi videbatur! cooptatio enim collegiorum ad populi beneficium transferebatur; atque is primus instituit in forum versus agere cum populo. Tamen illius vendibilem orationem religio deorum immortalium nobis defendentibus facile vincebat. Atque id actum est praetore me quinquennio ante quam consul sum factus; ita re magis quam summa auctoritate causa illa defensa est.

[97] Quod si in scaena, id est in contione, in qua rebus fictis et adumbratis loci plurimum est, tamen verum valet, si modo id patefactum et illustratum est, quid in amicitia fieri oportet, quae tota veritate perpenditur? in qua nisi, ut dicitur, apertum pectus videas tuumque ostendas, nihil fidum, nihil exploratum habeas, ne amare quidem aut amari, cum, id quam vere fiat, ignores. Quamquam ista assentatio, quamvis perniciosa sit, nocere tamen nemini potest nisi ei qui eam recipit atque ea delectatur. Ita fit, ut is assentatoribus patefaciat aures suas maxime, qui ipse sibi assentetur et se maxime ipse delectet.

[98] Omnino est amans sui virtus; optime enim se ipsa novit, quamque amabilis sit, intellegit. Ego autem non de virtute nunc loquor sed de virtutis opinione. Virtute enim ipsa non tam multi praediti esse quam videri volunt. Hos delectat assentatio, his fictus ad ipsorum voluntatem sermo cum adhibetur, orationem illam vanam testimonium esse laudum suarum putant. Nulla est igitur haec amicitia, cum alter verum audire non vult, alter ad mentiendum paratus est. Nec parasitorum in comoediis assentatio faceta nobis videretur, nisi essent milites gloriosi.

Magnas vero agere gratias Thais mihi?

Satis erat respondere: 'magnas'; 'ingentes' inquit. Semper auget assentator id, quod is cuius ad voluntatem dicitur vult esse magnum.

[99] Quam ob rem, quamquam blanda ista vanitas apud eos valet qui ipsi illam allectant et invitant, tamen etiam graviores constantioresque admonendi sunt, ut animadvertant, ne callida assentatione capiantur. Aperte enim adulantem nemo non videt, nisi qui admodum est excors; callidus ille et occultus ne se insinuet, studiose cavendum est; nec enim facillime agnoscitur, quippe qui etiam adversando saepe assentetur et litigare se simulans blandiatur atque ad extremum det manus vincique se patiatur, ut is qui illusus sit plus vidisse videatur. Quid autem turpius quam illudi? Quod ut ne accidat, magis cavendum est.

Ut me hodie ante omnes comicos stultos senes 
Versaris atque inlusseris lautissume.

[100] Haec enim etiam in fabulis stultissima persona est improvidorum et credulorum senum. Sed nescio quo pacto ab amicitiis perfectorum hominum, id est sapientium (de hac dico sapientia, quae videtur in hominem cadere posse), ad leves amicitias defluxit oratio. Quam ob rem ad illa prima redeamus eaque ipsa concludamus aliquando.

Virtus, virtus, inquam, C. Fanni, et tu, Q. Muci, et conciliat amicitias et conservat. In ea est enim convenientia rerum, in ea stabilitas, in ea constantia; quae cum se extulit et ostendit suum lumen et idem aspexit agnovitque in alio, ad id se admovet vicissimque accipit illud, quod in altero est; ex quo exardescit sive amor sive amicitia; utrumque enim dictum est ab amando; amare autem nihil est aliud nisi eum ipsum diligere, quem ames, nulla indigentia, nulla utilitate quaesita; quae tamen ipsa efflorescit ex amicitia, etiamsi tu eam minus secutus sis.

[101] Hac nos adulescentes benevolentia senes illos, L. Paulum, M. Catonem, C. Galum, P. Nasicam, Ti. Gracchum, Scipionis nostri socerum, dileximus, haec etiam magis elucet inter aequales, ut inter me et Scipionem, L. Furium, P. Rupilium, Sp. Mummium. Vicissim autem senes in adulescentium caritate acquiescimus, ut in vestra, ut in Q. Tuberonis; equidem etiam admodum adulescentis P. Rutili, A. Vergini familiaritate delector. Quoniamque ita ratio comparata est vitae naturaeque nostrae, ut alia ex alia aetas oriatur, maxime quidem optandum est, ut cum aequalibus possis, quibuscum tamquam e carceribus emissus sis, cum isdem ad calcem, ut dicitur, pervenire.

[102] Sed quoniam res humanae fragiles caducaeque sunt, semper aliqui anquirendi sunt quos diligamus et a quibus diligamur; caritate enim benevolentiaque sublata omnis est e vita sublata iucunditas. Mihi quidem Scipio, quamquam est subito ereptus, vivit tamen semperque vivet; virtutem enim amavi illius viri, quae exstincta non est; nec mihi soli versatur ante oculos, qui illam semper in manibus habui, sed etiam posteris erit clara et insignis. Nemo umquam animo aut spe maiora suscipiet, qui sibi non illius memoriam atque imaginem proponendam putet.

[103] Equidem ex omnibus rebus quas mihi aut fortuna aut natura tribuit, nihil habeo quod cum amicitia Scipionis possim comparare. In hac mihi de re publica consensus, in hac rerum privatarum consilium, in eadem requies plena oblectationis fuit. Numquam illum ne minima quidem re offendi, quod quidem senserim, nihil audivi ex eo ipse quod nollem; una domus erat, idem victus, isque communis, neque solum militia, sed etiam peregrinationes rusticationesque communes.

[104] Nam quid ego de studiis dicam cognoscendi semper aliquid atque discendi? in quibus remoti ab oculis populi omne otiosum tempus contrivimus. Quarum rerum recordatio et memoria si una cum illo occidisset, desiderium coniunctissimi atque amantissimi viri ferre nullo modo possem. Sed nec illa exstincta sunt alunturque potius et augentur cogitatione et memoria mea, et si illis plane orbatus essem, magnum tamen adfert mihi aetas ipsa solacium. Diutius enim iam in hoc desiderio esse non possum. Omnia autem brevia tolerabilia esse debent, etiamsi magna sunt.

Haec habui de amicitia quae dicerem. Vos autem hortor ut ita virtutem locetis, sine qua amicitia esse non potest, ut ea excepta nihil amicitia praestabilius putetis.

";

}