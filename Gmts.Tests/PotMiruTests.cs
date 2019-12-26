using FluentAssertions;
using Gmts.Models;
using Gmts.Processors;
using NUnit.Framework;

namespace Gmts.Tests
{
    public class PotMiruTests
    {
        internal static readonly string longDescription = @"<p><strong><img src=""https://borisk.splet.arnes.si/wp-content/blogs.dir/2002/files/slike-za-mojegc/thumbs/thumbs_slo_0.png?i=564931494"" alt width=""50"" height=""37"" style=""display:block;margin-left:auto;margin-right:auto;"" /></strong></p>
<p><strong>Sabotin</strong> je zaradi svoje lege odlična razgledna točka za širše območje Goriške in nudi pogled na Sveto Goro, Škabrijel, Vipavsko dolino, Furlansko nižino, Goriška Brda in Julijske Alpe. Zaradi burne zgodovine in naravoslovnih znamenitosti je pomembna izletniška točka, ki jo je vredno obiskati. Pohodnikom in kolesarjem je dostopen tudi pozimi, ko vrhove drugod po Sloveniji prekriva sneg.</p>
<p>Najbolj je Sabotin znan po tem, da je čezenj v prvi svetovni vojni potekala soška fronta. Zaradi bližine Gorice je bil <strong>ključna obrambna točka avstro-ogrske vojske</strong>, ki se je tod v letih 1915 in 1916 branila pred napadi italijanskih oboroženih sil. Zaradi <strong>strateške lege nad Sočo</strong> je bil pomembno avstro-ogrsko mostišče na desnem bregu reke. V 6. soški bitki avgusta 1916 ga je italijanska vojska zavzela, kar je omogočilo zasedbo Gorice. Hrib je <strong>prepreden s sistemi jarkov in kavern</strong>, ki sta jih obe vojski zgradili pri utrjevanju svojih položajev. Zlasti so zanimivi sistemi kavern na grebenu, ki so bili po omenjeni bitki preurejeni v topniške položaje. V bitkah na Sabotinu so bili udeleženi pripadniki številnih narodov, ki so se takrat bojevali v avstro-ogrskih ali italijanskih oboroženih silah.</p>
<p style=""text-align:center;""><strong><img src=""https://s3.amazonaws.com/gs-geo-images/280273be-4fee-48c1-b554-769394648b04_l.jpg"" alt width=""470"" height=""260"" /></strong></p>
<p style=""text-align:center;""><a href=""https://borisk.splet.arnes.si/wp-content/blogs.dir/2002/files/slike-za-mojegc/thumbs/thumbs_eng_0.png?i=1441703185""><img src=""https://borisk.splet.arnes.si/wp-content/blogs.dir/2002/files/slike-za-mojegc/thumbs/thumbs_eng_0.png?i=1441703185"" alt width=""50"" height=""37"" /></a></p>
<p><strong>Sabotin</strong> – the Park of Peace Due to its position Mt. Sabotin is a splendid vantage point for the wider area of the Goriška region and offers a view over the hills of Sveta Gora and Škabrijel, the Vipava valley, the Friuli lowlands, the Goriška Brda area and the Julian Alps. Because of its turbulent history and natural peculiarities it is an important tourist destination worth visiting. It is accessible to trekkers and bikers in wintertime too, when other mountains elsewhere in Slovenia are covered with snow.</p>
<p>Sabotin is best known for being an important stretch of <strong>the Isonzo Front</strong> during the First World War. Because of its location overlooking the town of Gorizia, it was <strong>a key defensive point for the Austro-Hungarian army</strong> against Italian attacks in 1915 and 1916. Due to its strategic position above the Soča, this hill near Gorica/Gorizia was an important Austro-Hungarian stronghold on the right river bank. In the sixth Soča battle in August 1916, Italian troops took Sabotin, thereby securing their advance to Gorizia. The hill is crisscrossed by a system of trenches and caves built by the Austro-Hungarian and Italian armies to fortify their positions. Particularly interesting are <strong>the systems of caves on the ridge</strong>, which after the battle were used as artillery positions. Soldiers from many a nation participated in the battles on Sabotin, fighting for either the Italian or the Austro-Hungarian side.</p>
<p style=""text-align:center;""><strong>ZAKLAD SE NE NAHAJA NA DANIH KOORDINATAH !</strong></p>
<p style=""text-align:center;""><em>Pisalo prinesi s seboj !</em></p>
<p style=""text-align:center;""><span class=""tlid-translation translation"" lang=""en""><strong><span title>CACH IS NOT AVAILABLE AT THE KNOWN COORDINATES !</span></strong></span></p>
<p style=""text-align:center;""><em><span class=""tlid-translation translation"" lang=""en""><span class title>Bring your own pen !</span></span></em></p>
<p style=""text-align:center;""><span style=""color:#008080;""><em><span class=""tlid-translation translation"" lang=""en""><span class title>**********************************</span></span></em></span></p>
<h2 style=""text-align:center;""><strong>Ključ/Key: <span style=""color:#ff0000;"">DU</span></strong></h2>
<h2 style=""text-align:center;""><strong>[N]<span style=""color:#0000ff;"">MN NR.SRN</span> [E]<span style=""color:#0000ff;"">SJL LP.LMQ</span></strong></h2>
<p style=""text-align:center;""><span style=""color:#008080;""><em><span class=""tlid-translation translation"" lang=""en""><span class title>*****************************************</span></span></em></span></p>
<p style=""text-align:left;""><span style=""color:#ff0000;""><strong>Pripomoček / <span class=""tlid-translation translation"" lang=""en""><span class title>Accessories</span></span></strong> :<br /></span></p>
<p><img src=""https://borisk.splet.arnes.si/wp-content/blogs.dir/2002/files/9-brda-mystery/mysterywheel_brdateam.jpg"" alt width=""764"" height=""680"" /></p>".Replace("\r\n", "\n");

        private PotMiruProcessor processor;

        [SetUp]
        public void SetUp()
        {
            processor = new PotMiruProcessor();
        }

        [Test]
        public void ParseKeyLatLng()
        {
            var keyLatLng = processor.ParseKeyLatLng(longDescription);

            var expected = ("DU", "MN NR.SRN", "SJL LP.LMQ");
            keyLatLng.Should().BeEquivalentTo(expected);
        }

        [TestCase("D3", ExpectedResult = 25)]
        [TestCase("DA", ExpectedResult = 33)]
        public int CalculateOffset(string key)
        {
            return processor.CalculateOffset(key);
        }

        [Test]
        public void DecodeString()
        {
            var offset = processor.CalculateOffset("D3");

            var decoded = processor.DecodeString("GJK BFG", offset);

            var expected = "690 156";
            decoded.Should().Be(expected);
        }

        [Test]
        public void ParseCoordinates()
        {
            var parsed = processor.ParseCoordinates("45 59.403");

            var expected = 45.99005;
            parsed.Should().Be(expected);
        }

        [Test]
        public void ProcessCacheData()
        {
            var cacheData = new CacheData("GC87CEJ", new LatLng(45.99005, 13.6121), longDescription);

            var processed = processor.Process(cacheData);

            var expected = new ProcessedCacheData(cacheData, new LatLng(45.984917, 13.622467));
            processed.Should().BeEquivalentToProcessedCacheData(expected);
        }
    }
}
