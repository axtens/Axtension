namespace Axtension
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;

    public static class CountryCodeTopLevelDomain
    {
        private static string cctld = @"{
            ac : ['com', 'net', 'gov', 'org', 'mil'],
            ad : ['nom'],
            ae : ['co', 'net', 'gov', 'ac', 'sch', 'org', 'mil', 'pro', 'name'],
            af : ['com', 'edu', 'gov', 'net', 'org'],
            ag : ['com', 'org', 'net', 'co', 'nom'],
            ai : ['off', 'com', 'net', 'org'],
            al : ['com', 'edu', 'gov', 'mil', 'net', 'org'],
            am : ['com', 'net', 'org'],
            ao : ['ed', 'gv', 'og', 'co', 'pb', 'it'],
            aq : [],
            ar : ['com', 'edu', 'gob', 'gov', 'gov', 'int', 'mil', 'net', 'org', 'tur'],
            'as' : ['gov'],
            at : ['gv', 'ac', 'co', 'or', 'priv'],
            au : ['com', 'net', 'org', 'edu', 'gov', 'csiro', 'asn', 'id', 'info', 'conf', 'oz', 'act', 'nsw', 'nt', 'qld', 'sa', 'tas', 'vic', 'wa'],
            aw : ['com'],
            ax : [],
            az : ['com', 'net', 'int', 'gov', 'org', 'edu', 'info', 'pp', 'mil', 'name', 'pro', 'biz', 'co'],
            ba : ['org', 'net', 'edu', 'gov', 'mil', 'unsa', 'untz', 'unmo', 'unbi', 'unze', 'co', 'com', 'rs'],
            bb : ['co', 'com', 'net', 'org', 'gov', 'edu', 'info', 'store', 'tv', 'biz'],
            bd : ['com', 'edu', 'gov', 'net', 'mil', 'org', 'ac'],
            be : ['ac'],
            bf : ['gov'],
            bg : ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'],
            bh : ['com', 'info', 'cc', 'edu', 'biz', 'net', 'org', 'gov'],
            bi : ['co', 'com', 'edu', 'or', 'org'],
            bj : ['gouv', 'mil', 'edu', 'gov', 'assoc', 'barreau', 'com'],
            bm : ['com', 'edu', 'gov', 'net', 'org'],
            bn : ['com', 'edu', 'gov', 'net', 'org'],
            bo : ['com', 'net', 'org', 'tv', 'mil', 'int', 'gob', 'gov', 'edu'],
            br : ['adm', 'adv', 'agr', 'am', 'arq', 'art', 'ato', 'b', 'bio', 'blog', 'bmd', 'cim', 'cng', 'cnt', 'com', 'coop', 'ecn', 'edu', 'eng', 'esp', 'etc', 'eti', 'far', 'flog', 'fm', 'fnd', 'fot', 'fst', 'g12', 'ggf', 'gov', 'imb', 'ind', 'inf', 'jor', 'jus', 'lel', 'mat', 'med', 'mil', 'mus', 'net', 'nom', 'not', 'ntr', 'odo', 'org', 'ppg', 'pro', 'psc', 'psi', 'qsl', 'radio', 'rec', 'slg', 'srv', 'taxi', 'teo', 'tmp', 'trd', 'tur', 'tv', 'vet', 'vlog', 'wiki', 'zlg'],
            bs : ['com', 'net', 'org', 'edu', 'gov', 'we'],
            bt : ['com', 'edu', 'gov', 'net', 'org'],
            bw : [],
            by : ['gov', 'mil'],
            bz : ['com', 'edu', 'gov', 'net', 'org'],
            ca : ['ab', 'bc', 'mb', 'nb', 'nf', 'nl', 'ns', 'nt', 'nu', 'on', 'pe', 'qc', 'sk', 'yk'],
            cc : ['com', 'net', 'edu', 'org', 'gov'],
            cd : ['com', 'net', 'edu', 'org', 'gov'],
            cf : [],
            cg : [],
            ch : ['02'],
            ci : ['org', 'or', 'com', 'co', 'edu', 'ed', 'ac', 'net', 'go', 'asso', 'aeroport', 'int', 'presse'],
            ck : ['co', 'org', 'edu', 'gov', 'net', 'gen', 'biz', 'info'],
            cm : [],
            cn : ['ac', 'com', 'edu', 'gov', 'mil', 'net', 'org', 'ah', 'bj', 'cq', 'fj', 'gd', 'gs', 'gz', 'gx', 'ha', 'hb', 'he', 'hi', 'hl', 'hn', 'jl', 'js', 'jx', 'ln', 'nm', 'nx', 'qh', 'sc', 'sd', 'sh', 'sn', 'sx', 'tj', 'tw', 'xj', 'xz', 'yn', 'zj'],
            co : ['com', 'org', 'edu', 'gov', 'net', 'mil', 'nom'],
            cr : ['ac', 'co', 'ed', 'fi', 'go', 'or', 'sa'],
            cu : ['com', 'edu', 'org', 'net', 'gov', 'inf'],
            cv : ['net', 'gov', 'org', 'edu', 'int', 'publ', 'com', 'nome'],
            cw : [],
            cy : ['ac', 'net', 'gov', 'org', 'pro', 'name', 'ekloges', 'tm', 'ltd', 'biz', 'press', 'parliament', 'com'],
            cz : [],
            de : [],
            dj : [],
            dk : [],
            dm : ['com', 'net', 'org'],
            'do' : ['edu', 'gob', 'gov', 'com', 'sld', 'org', 'net', 'web', 'mil', 'art'],
            dz : ['com', 'org', 'net', 'gov', 'edu', 'asso', 'pol', 'art'],
            ec : ['com', 'info', 'net', 'fin', 'med', 'pro', 'org', 'edu', 'gov', 'mil'],
            eg : ['com', 'edu', 'eun', 'gov', 'mil', 'name', 'net', 'org', 'sci'],
            er : ['com', 'edu', 'gov', 'mil', 'net', 'org', 'ind', 'rochest', 'w'],
            es : ['com', 'nom', 'org', 'gob', 'edu'],
            et : ['com', 'gov', 'org', 'edu', 'net', 'biz', 'name', 'info'],
            fj : ['ac', 'biz', 'com', 'info', 'mil', 'name', 'net', 'org', 'pro'],
            fk : ['co', 'org', 'gov', 'ac', 'nom', 'net'],
            fr : ['tm', 'asso', 'nom', 'prd', 'presse', 'com', 'gouv'],
            gg : ['co', 'net', 'org'],
            gh : ['com', 'edu', 'gov', 'org', 'mil'],
            gn : ['com', 'ac', 'gov', 'org', 'net'],
            gr : ['com', 'edu', 'net', 'org', 'gov', 'mil'],
            gt : ['com', 'edu', 'net', 'gob', 'org', 'mil', 'ind'],
            gu : ['com', 'net', 'gov', 'org', 'edu'],
            hk : ['com', 'edu', 'gov', 'idv', 'net', 'org'],
            id : ['ac', 'co', 'net', 'or', 'web', 'sch', 'mil', 'go', 'war.net'],
            il : ['ac', 'co', 'org', 'net', 'k12', 'gov', 'muni', 'idf'],
            'in' : ['co', 'firm', 'net', 'org', 'gen', 'ind', 'ac', 'edu', 'res', 'ernet', 'gov', 'mil', 'nic', 'nic'],
            iq : ['gov', 'edu', 'com', 'mil', 'org', 'net'],
            ir : ['ac', 'co', 'gov', 'id', 'net', 'org', 'sch', 'dnssec'],
            it : ['gov', 'edu'],
            je : ['co', 'net', 'org'],
            jo : ['com', 'net', 'gov', 'edu', 'org', 'mil', 'name', 'sch'],
            jp : ['ac', 'ad', 'co', 'ed', 'go', 'gr', 'lg', 'ne', 'or'],
            ke : ['co', 'or', 'ne', 'go', 'ac', 'sc', 'me', 'mobi', 'info'],
            kh : ['per', 'com', 'edu', 'gov', 'mil', 'net', 'org'],
            ki : ['com', 'biz', 'de', 'net', 'info', 'org', 'gov', 'edu', 'mob', 'tel'],
            km : ['com', 'coop', 'asso', 'nom', 'presse', 'tm', 'medecin', 'notaires', 'pharmaciens', 'veterinaire', 'edu', 'gouv', 'mil'],
            kn : ['net', 'org', 'edu', 'gov'],
            kr : ['co', 'ne', 'or', 're', 'pe', 'go', 'mil', 'ac', 'hs', 'ms', 'es', 'sc', 'kg', 'seoul', 'busan', 'daegu', 'incheon', 'gwangju', 'daejeon', 'ulsan', 'gyeonggi', 'gangwon', 'chungbuk', 'chungnam', 'jeonbuk', 'jeonnam', 'gyeongbuk', 'gyeongnam', 'jeju'],
            kw : ['edu', 'com', 'net', 'org', 'gov'],
            ky : ['com', 'org', 'net', 'edu', 'gov'],
            kz : ['com', 'edu', 'gov', 'mil', 'net', 'org'],
            lb : ['com', 'edu', 'gov', 'net', 'org'],
            lk : ['gov', 'sch', 'net', 'int', 'com', 'org', 'edu', 'ngo', 'soc', 'web', 'ltd', 'assn', 'grp', 'hotel'],
            lr : ['com', 'edu', 'gov', 'org', 'net'],
            lv : ['com', 'edu', 'gov', 'org', 'mil', 'id', 'net', 'asn', 'conf'],
            ly : ['com', 'net', 'gov', 'plc', 'edu', 'sch', 'med', 'org', 'id'],
            ma : ['net', 'ac', 'org', 'gov', 'press', 'co'],
            mc : ['tm', 'asso'],
            me : ['co', 'net', 'org', 'edu', 'ac', 'gov', 'its', 'priv'],
            mg : ['org', 'nom', 'gov', 'prd', 'tm', 'edu', 'mil', 'com'],
            mk : ['com', 'org', 'net', 'edu', 'gov', 'inf', 'name', 'pro'],
            ml : ['com', 'net', 'org', 'edu', 'gov', 'presse'],
            mn : ['gov', 'edu', 'org'],
            mo : ['com', 'edu', 'gov', 'net', 'org'],
            mt : ['com', 'org', 'net', 'edu', 'gov'],
            mv : ['aero', 'biz', 'com', 'coop', 'edu', 'gov', 'info', 'int', 'mil', 'museum', 'name', 'net', 'org', 'pro'],
            mw : ['ac', 'co', 'com', 'coop', 'edu', 'gov', 'int', 'museum', 'net', 'org'],
            mx : ['com', 'net', 'org', 'edu', 'gob'],
            my : ['com', 'net', 'org', 'gov', 'edu', 'sch', 'mil', 'name'],
            nf : ['com', 'net', 'arts', 'store', 'web', 'firm', 'info', 'other', 'per', 'rec'],
            ng : ['com', 'org', 'gov', 'edu', 'net', 'sch', 'name', 'mobi', 'biz', 'mil'],
            ni : ['gob', 'co', 'com', 'ac', 'edu', 'org', 'nom', 'net', 'mil'],
            np : ['com', 'edu', 'gov', 'org', 'mil', 'net'],
            nr : ['edu', 'gov', 'biz', 'info', 'net', 'org', 'com'],
            om : ['com', 'co', 'edu', 'ac', 'sch', 'gov', 'net', 'org', 'mil', 'museum', 'biz', 'pro', 'med'],
            pe : ['edu', 'gob', 'nom', 'mil', 'sld', 'org', 'com', 'net'],
            ph : ['com', 'net', 'org', 'mil', 'ngo', 'i', 'gov', 'edu'],
            pk : ['com', 'net', 'edu', 'org', 'fam', 'biz', 'web', 'gov', 'gob', 'gok', 'gon', 'gop', 'gos'],
            pl : ['pwr', 'com', 'biz', 'net', 'art', 'edu', 'org', 'ngo', 'gov', 'info', 'mil', 'waw', 'warszawa', 'wroc', 'wroclaw', 'krakow', 'katowice', 'poznan', 'lodz', 'gda', 'gdansk', 'slupsk', 'radom', 'szczecin', 'lublin', 'bialystok', 'olsztyn', 'torun', 'gorzow', 'zgora'],
            pr : ['biz', 'com', 'edu', 'gov', 'info', 'isla', 'name', 'net', 'org', 'pro', 'est', 'prof', 'ac'],
            ps : ['com', 'net', 'org', 'edu', 'gov', 'plo', 'sec'],
            pw : ['co', 'ne', 'or', 'ed', 'go', 'belau'],
            ro : ['arts', 'com', 'firm', 'info', 'nom', 'nt', 'org', 'rec', 'store', 'tm', 'www'],
            rs : ['co', 'org', 'edu', 'ac', 'gov', 'in'],
            sb : ['com', 'net', 'edu', 'org', 'gov'],
            sc : ['com', 'net', 'edu', 'gov', 'org'],
            sh : ['co', 'com', 'org', 'gov', 'edu', 'net', 'nom'],
            sl : ['com', 'net', 'org', 'edu', 'gov'],
            st : ['gov', 'saotome', 'principe', 'consulado', 'embaixada', 'org', 'edu', 'net', 'com', 'store', 'mil', 'co'],
            sv : ['edu', 'gob', 'com', 'org', 'red'],
            sz : ['co', 'ac', 'org'],
            tr : ['com', 'gen', 'org', 'biz', 'info', 'av', 'dr', 'pol', 'bel', 'tsk', 'bbs', 'k12', 'edu', 'name', 'net', 'gov', 'web', 'tel', 'tv'],
            tt : ['co', 'com', 'org', 'net', 'biz', 'info', 'pro', 'int', 'coop', 'jobs', 'mobi', 'travel', 'museum', 'aero', 'cat', 'tel', 'name', 'mil', 'edu', 'gov'],
            tw : ['edu', 'gov', 'mil', 'com', 'net', 'org', 'idv', 'game', 'ebiz', 'club'],
            mu : ['com', 'gov', 'net', 'org', 'ac', 'co', 'or'],
            mz : ['ac', 'co', 'edu', 'org', 'gov'],
            na : ['com', 'co'],
            nz : ['ac', 'co', 'cri', 'geek', 'gen', 'govt', 'health', 'iwi', 'maori', 'mil', 'net', 'org', 'parliament', 'school'],
            pa : ['abo', 'ac', 'com', 'edu', 'gob', 'ing', 'med', 'net', 'nom', 'org', 'sld'],
            pt : ['com', 'edu', 'gov', 'int', 'net', 'nome', 'org', 'publ'],
            py : ['com', 'edu', 'gov', 'mil', 'net', 'org'],
            qa : ['com', 'edu', 'gov', 'mil', 'net', 'org'],
            re : ['asso', 'com', 'nom'],
            ru : ['ac', 'adygeya', 'altai', 'amur', 'arkhangelsk', 'astrakhan', 'bashkiria', 'belgorod', 'bir', 'bryansk', 'buryatia', 'cbg', 'chel', 'chelyabinsk', 'chita', 'chita', 'chukotka', 'chuvashia', 'com', 'dagestan', 'e-burg', 'edu', 'gov', 'grozny', 'int', 'irkutsk', 'ivanovo', 'izhevsk', 'jar', 'joshkar-ola', 'kalmykia', 'kaluga', 'kamchatka', 'karelia', 'kazan', 'kchr', 'kemerovo', 'khabarovsk', 'khakassia', 'khv', 'kirov', 'koenig', 'komi', 'kostroma', 'kranoyarsk', 'kuban', 'kurgan', 'kursk', 'lipetsk', 'magadan', 'mari', 'mari-el', 'marine', 'mil', 'mordovia', 'mosreg', 'msk', 'murmansk', 'nalchik', 'net', 'nnov', 'nov', 'novosibirsk', 'nsk', 'omsk', 'orenburg', 'org', 'oryol', 'penza', 'perm', 'pp', 'pskov', 'ptz', 'rnd', 'ryazan', 'sakhalin', 'samara', 'saratov', 'simbirsk', 'smolensk', 'spb', 'stavropol', 'stv', 'surgut', 'tambov', 'tatarstan', 'tom', 'tomsk', 'tsaritsyn', 'tsk', 'tula', 'tuva', 'tver', 'tyumen', 'udm', 'udmurtia', 'ulan-ude', 'vladikavkaz', 'vladimir', 'vladivostok', 'volgograd', 'vologda', 'voronezh', 'vrn', 'vyatka', 'yakutia', 'yamal', 'yekaterinburg', 'yuzhno-sakhalinsk'],
            rw : ['ac', 'co', 'com', 'edu', 'gouv', 'gov', 'int', 'mil', 'net'],
            sa : ['com', 'edu', 'gov', 'med', 'net', 'org', 'pub', 'sch'],
            sd : ['com', 'edu', 'gov', 'info', 'med', 'net', 'org', 'tv'],
            se : ['a', 'ac', 'b', 'bd', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'k', 'l', 'm', 'n', 'o', 'org', 'p', 'parti', 'pp', 'press', 'r', 's', 't', 'tm', 'u', 'w', 'x', 'y', 'z'],
            sg : ['com', 'edu', 'gov', 'idn', 'net', 'org', 'per'],
            sn : ['art', 'com', 'edu', 'gouv', 'org', 'perso', 'univ'],
            sy : ['com', 'edu', 'gov', 'mil', 'net', 'news', 'org'],
            th : ['ac', 'co', 'go', 'in', 'mi', 'net', 'or'],
            tj : ['ac', 'biz', 'co', 'com', 'edu', 'go', 'gov', 'info', 'int', 'mil', 'name', 'net', 'nic', 'org', 'test', 'web'],
            tn : ['agrinet', 'com', 'defense', 'edunet', 'ens', 'fin', 'gov', 'ind', 'info', 'intl', 'mincom', 'nat', 'net', 'org', 'perso', 'rnrt', 'rns', 'rnu', 'tourism'],
            tz : ['ac', 'co', 'go', 'ne', 'or'],
            ua : ['biz', 'cherkassy', 'chernigov', 'chernovtsy', 'ck', 'cn', 'co', 'com', 'crimea', 'cv', 'dn', 'dnepropetrovsk', 'donetsk', 'dp', 'edu', 'gov', 'if', 'in', 'ivano-frankivsk', 'kh', 'kharkov', 'kherson', 'khmelnitskiy', 'kiev', 'kirovograd', 'km', 'kr', 'ks', 'kv', 'lg', 'lugansk', 'lutsk', 'lviv', 'me', 'mk', 'net', 'nikolaev', 'od', 'odessa', 'org', 'pl', 'poltava', 'pp', 'rovno', 'rv', 'sebastopol', 'sumy', 'te', 'ternopil', 'uzhgorod', 'vinnica', 'vn', 'zaporizhzhe', 'zhitomir', 'zp', 'zt'],
            ug : ['ac', 'co', 'go', 'ne', 'or', 'org', 'sc'],
            uk : ['ac', 'bl', 'british-library', 'co', 'cym', 'gov', 'govt', 'icnet', 'jet', 'lea', 'ltd', 'me', 'mil', 'mod', 'mod', 'national-library-scotland', 'nel', 'net', 'nhs', 'nhs', 'nic', 'nls', 'org', 'orgn', 'parliament', 'parliament', 'plc', 'police', 'sch', 'scot', 'soc'],
            us : ['dni', 'fed', 'isa', 'kids', 'nsn'],
            uy : ['com', 'edu', 'gub', 'mil', 'net', 'org'],
            ve : ['co', 'com', 'edu', 'gob', 'info', 'mil', 'net', 'org', 'web'],
            vi : ['co', 'com', 'k12', 'net', 'org'],
            vn : ['ac', 'biz', 'com', 'edu', 'gov', 'health', 'info', 'int', 'name', 'net', 'org', 'pro'],
            ye : ['co', 'com', 'gov', 'ltd', 'me', 'net', 'org', 'plc'],
            yu : ['ac', 'co', 'edu', 'gov', 'org'],
            za : ['ac', 'agric', 'alt', 'bourse', 'city', 'co', 'cybernet', 'db', 'edu', 'gov', 'grondar', 'iaccess', 'imt', 'inca', 'landesign', 'law', 'mil', 'net', 'ngo', 'nis', 'nom', 'olivetti', 'org', 'pix', 'school', 'tm', 'web'],
            zm : ['ac', 'co', 'com', 'edu', 'gov', 'net', 'org', 'sch']}";

        private static Dictionary<string, object> dictTLD = CountryCodeTopLevelDomain.CountryCodeTopLevelDomains();
        private static Dictionary<string, string> result = new Dictionary<string, string>();


        public static Dictionary<string, object> CountryCodeTopLevelDomains()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return ser.Deserialize<Dictionary<string, object>>(cctld);
        }

        public static object DecodeJson(string json) //Dictionary<string, object>
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ser.MaxJsonLength = 8388608; //8MB
            if (json.Substring(0, 1) == "[")
            {
                return ser.Deserialize<List<object>>(json);
            }
            else
            {
                return ser.Deserialize<Dictionary<string, object>>(json);
            }
        }

        public static Dictionary<string, string> ParseHost(string host)
        {
            result["DL1"] = string.Empty;
            result["DL2"] = string.Empty;
            result["DL3"] = string.Empty;
            result["DL4"] = string.Empty;
            result["MSG"] = string.Empty;

            if (string.Empty == host)
            {
                result["MSG"] = "NO TEXT";
                return result;
            }

            int colonPos = host.IndexOf(":");
            if (-1 != colonPos)
            {
                host = host.Substring(0, colonPos);
            }

            var hostList = host.Split('.');
            if (1 == hostList.Length)
            {
                result["DL1"] = string.Empty;
                result["DL2"] = string.Empty;
                result["DL3"] = host;
                result["DL4"] = string.Empty;
                result["MSG"] = "SINGLE WORD HOST";
                return result;
            }

            Queue<string> queueReverse = new Queue<string>();
            for (var i = hostList.Length - 1; i >= 0; i--)
            {
                queueReverse.Enqueue(hostList[i]);
            }

            if (IsIpNumber(queueReverse.ToArray()))
            {
                result["DL1"] = string.Empty;
                result["DL2"] = string.Empty;
                result["DL3"] = host;
                result["DL4"] = string.Empty;
                result["MSG"] = "IP ADDRESS";
                return result;
            }

            if (queueReverse.Peek().Length > 2)
            {
                result["DL1"] = string.Empty;
                result["DL2"] = queueReverse.Dequeue();
                result["DL3"] = queueReverse.Dequeue();
                string[] remainder = queueReverse.ToArray();
                Array.Reverse(remainder);
                result["DL4"] = string.Join(".", remainder);
                result["MSG"] = "OK";
                return result;
            }

            if (!dictTLD.ContainsKey(queueReverse.Peek().ToString()))
            {
                result["DL1"] = string.Empty;
                result["DL2"] = string.Empty;
                result["DL3"] = string.Empty;
                result["DL4"] = string.Empty;
                result["MSG"] = "UNKNOWN COUNTRY";
                return result;
            }

            string country = queueReverse.Dequeue();
            ArrayList dl2 = new ArrayList();
            dl2 = (ArrayList)dictTLD[country];
            bool noEntities = dl2.Count < 1;
            bool entityNotFound = dl2.IndexOf(queueReverse.Peek().ToString()) == -1;
            if (noEntities || entityNotFound)
            {
                result["DL1"] = country;
                result["DL2"] = string.Empty;
                result["DL3"] = queueReverse.Dequeue();
                string[] remainder = queueReverse.ToArray();
                Array.Reverse(remainder);
                result["DL4"] = string.Join(".", remainder);
                result["MSG"] = "OK";
                return result;
            }
            else
            {
                result["DL1"] = country;
                result["DL2"] = queueReverse.Dequeue();
                result["DL3"] = queueReverse.Dequeue();
                string[] remainder = queueReverse.ToArray();
                Array.Reverse(remainder);
                result["DL4"] = string.Join(".", remainder);
                result["MSG"] = "OK";
                return result;
            }
        }

        public static bool IsIpNumber(string[] host)
        {
            int itemp = 0;
            if (4 != host.Length)
            {
                return false;
            }

            for (var i = 0; i < host.Length; i++)
            {
                string x = host[i];
                if (!int.TryParse(x, out itemp)) 
                {
                    return false;
                }
            }

            return true;
        }
    }
}
