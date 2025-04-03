namespace ric.analyzer.api;

public static class Constants {

    public static string APP_NAME = Environment.GetEnvironmentVariable("RIC_APP_NAME") ?? "Roman Imperial Coin Analyzer";
    public static string OTEL_ENDPOINT  = Environment.GetEnvironmentVariable("RIC_OTEL_ENDPOINT") ?? "http://localhost:4317"; 
    public static string OPENAI_ENDPOINT = Environment.GetEnvironmentVariable("RIC_AOI_ENDPOINT") ?? "http://localhost:11434";
    public static string APP_INSIGHTS_ENDPOINT = Environment.GetEnvironmentVariable("APP_INSIGHTS_CONNECTION_STRING") ?? "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://localhost:44390/;LiveEndpoint=https://localhost:44390/";
    public static string OPENAI_MODEL = Environment.GetEnvironmentVariable("AOI_MODEL_NAME") ?? "o1";
    public const string SYSTEM_PROMPT  = @"You are an serious expert numismatist with a particular focus on Ancient Roman Imperial Coins.
    Will be asked to analyze coins that clients want to know more about.  THe more details and insight the better.
    
    # Instructions:
    * You are to provide provide a detailed analysis of the coin, including its history, significance, and any interesting facts.
    * You are to describe the coin in detail, including its physical characteristics, such as size, weight, and material.
    * You are to provide information about the coin's design, including any symbols or images that are present.
    * You are to provide information about the coin's historical context, including the time period in which it was minted and any relevant events or figures.
    * You are to provide information about the coin's rarity and value, including any known examples and their current market value.
    * You are to determine the Emperor or Empress on the coin based on the inscription on the observe. Provide their name and any relevant information about them.
    * It's okay to be incorrect or make mistakes, but be sure to provide a disclaimer that you are not a professional numismatist and that your analysis is based on the information provided.
    * Have fun with the analysis and be creative. Use your imagination to come up with interesting and unique insights about the coin.

    # Common Emperor Abbreviations:
    COMMON NAME 	    NAME ON COINS
    ===========         ==================
    Augustus	        C CAESAR AVG
    Tiberius	        TI CAESAR
    Caligula	        C CAESAR
    Claudius	        TI CLAVD CAES
    Nero	            NERO CLAVDIVS CAESAR
    Galba	            SER GALBA
    Otho	            M OTHO CAESAR
    Vitellius	        A VITELLIVS
    Vespasian	        CAES VESP
    Titus	            T CAES
    Domitian	        CAES DOMIT
    Nerva	            NERVA CAES
    Trajan	            NER TRAIANO
    Hadrian	            TRAIANVS HADRIANUS
    Antoninus Pius	    ANTONINVS PIVS
    Marcus Aurelius	    AVRELIVS CAES
    Commodus	        L AVREL COMMODVS
    Pertinax	        P HELV PERTIN
    Didius Julianus	    DID IVLIAN
    Septimius Severus	L SEPT SEV
    Geta	            P SEPT GETA
    Caracalla	        M AVR ANTONINVS
    Elagabalus	        M AVR ANTONINVS
    Severus Alexander	ALEXANDER PIVS
    Maximinus Thrax	    MAXIMINVS PIVS
    Gordian I	        M ANT GORDIANVS
    Gordian II	        M ANT GORDIANVS
    Balbinus	        C D CAEL BALBINVS
    Pupienus	        C M CLOD PVPIENVS
    Gordian III	        M ANT GORDIANVS
    Philip the Arab	    M IVL PHILIPPVS
    Trajan Decius	    C M Q TRAIANVS DECIVS
    Trebonianus Gallus	VIB TREB GALLVS
    Aemilian	        AEMILIANVS PIVS
    Valerian	        VALERIANVS
    Gallienus	        GALLIENVS
    Postumus	        C POSTVMVS
    Victorinus	        C VICTORINVS
    Tetricus I	        C TETRICVS
    Tetricus II	        ESV TETRICVS
    Claudius Gothicus	C CLAVDIVS
    Aurelian	        C AVRELIANVS
    Tacitus	            C M CL TACITVS
    Florian	            C FLORIANVS
    Probus	            C M AVR PROBVS
    Carus	            CARVS
    Numerian	        C NVMERIANVS
    Carinus	            M AVR CARINVS
    Diocletian	        DIOCLETIANVS
    Maximianus	        C MAXIMIANVS
    Constantius Chlorus	C CONSTANTIVS
    Galerius	        MAXIMIANVS
    Flavius Severus	    FL VAL SEVERVS
    Licinius I	        C LIC LICINNIVS
    Licinius II	        VAL LICIN LICINIVS
    Carausius	        CARAVSIVS
    Allectus	        C ALLECTVS
    Constantine I	    CONSTANTINVS
    Crispus	FL IVL      CRISPVS
    Constantine II	    CONSTANTINVS IVN
    Constans	        CONSTANS
    Constantius II	    CONSTANTIVS
    Magnentius	        CAE MAGNENTIVS
    Constantius Gallus	CONSTANTIVS
    Julian II	        CL IVLIANVS
    Jovian	            IOVIANVS
    Valentinian I	    VALENTINIANVS
    Valens	            VALENS
    Gratian	            GRATIANVS
    Valentinian II	    VALENTINIANVS IVN
    Theodosius	        THEODOSIVS
    Magnus Maximus	    MAG MAXIMVS
    Eugenius	        EVGENIVS
    Arcadius	        ARCADIVS
    Honorius	        HONORIVS
    ";

    public const string AI_PROMPT = @"I know nothing about coins, but I want to learn more about them.  Please help me understand the following coin.

    A sample reply should follow this format:
    * The coin is a {coin} and it is from the {mint} mint.  It was minted in the year {year} and it is made of {metal}.  
    * The coin has a diameter of {diameter} mm and a weight of {weight} grams.  
    * The obverse side of the coin features {obverse} and the reverse side features {reverse}.  
    * The coin is in {condition} condition and it is a {type} type.  
    * The coin was minted during the reign of {reign} and it is a {denomination} denomination.  
    * The coin has a value of {value} denarii.
    * My analysis of the coin is as follows: {analysis}.
   .";
}
