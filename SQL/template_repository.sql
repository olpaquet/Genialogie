use Genealogie
--select * from v_tables

declare @yes nvarchar(max);
declare @ttype varchar(100);

declare @nomtable varchar(250) = 'Role'
DECLARE @idcol int
declare @nomcol VARCHAR(250)
declare @typecol nVARCHAR(25)
declare @longueurmax int
declare @precision int
declare @scale int
declare @estnullable int
declare @yescreate varchar(max)
declare @yesmodify varchar(max)
declare @val varchar(max)
declare @return varchar(max)
declare @clef varchar(max)
declare @cleftype varchar(max)

DECLARE c CURSOR FOR
    SELECT  idcol, nomcol, typecol, longueurmax, precision, scale, estnullable FROM v_tables
	where nomtable = @nomtable
	order by idcol
 
OPEN c
 
FETCH c INTO @idcol, @nomcol, @typecol, @longueurmax, @precision, @scale, @estnullable
 

set @yes = '';
set @yescreate = '';
set @yesmodify = '';

WHILE @@FETCH_STATUS = 0
BEGIN

	select @ttype = dbo.DonnerTypeCSharp(@typecol, @estnullable)
	/*select @ttype = CASE
		WHEN @typecol = 'varchar' THEN 'string'  
		when @typecol = 'money' then 'decimal'
		when @typecol = 'datetime' then 'DateTime'  
		ELSE @typecol
	end

	if @estnullable = 1 and @ttype in ('int','DateTime','decimal')
		set @ttype = concat(@ttype,'?');*/

	set @yes = concat(@yes,'public',' ', @ttype,' ',@nomcol,' {get; set;}');
	if @yescreate <> ''
		set @yescreate = concat(@yescreate, char(10))
	if @yesmodify <> ''
		set @yesmodify = concat(@yesmodify, char(10))

	if @idcol = 1
	begin
		set @clef = @nomcol
		set @cleftype = @ttype
		set @val = ',null,true'
		set @return = concat('return (int)com.Parametres["',@clef,'"].Valeur;')
	end
	else
		set @val = concat(',e.',@nomcol)
	set @yescreate = concat(@yescreate, 'com.AjouterParametre("', @nomcol, '"',@val,');')
	FETCH c INTO  @idcol, @nomcol, @typecol, @longueurmax, @precision, @scale, @estnullable
	
END
 
CLOSE c
DEALLOCATE c

print concat('public int Creer(',@nomtable,' ','e){')
print concat('Commande com = new Commande("',@nomtable,'_cre",true);')
print @yescreate
print '_connexion.ExecuterNonRequete(com);'
print @return
print '}'

print concat('public bool Modifier(',@cleftype,' ',@clef,',',' ',@nomtable,' ','e','){')
print concat('Commande com = new Commande("',@nomtable,'_mod",true);')
print @yescreate + '...'
print 'return (int)_connexion.ExecuterNonRequete(com)>0;'
print '}'

print concat('public bool Supprimer(',@cleftype,' ',@clef,'){')
print concat('Commande com = new Commande("',@nomtable,'_eff",true);')
print concat('com.AjouterParametre("',@clef,'",',@clef,');')
print 'return (int)_connexion.ExecuterNonRequete(com)>0;'
print '}'

print concat('public bool Activer(',@cleftype,' ',@clef,'){')
print concat('Commande com = new Commande("',@nomtable,'_act",true);')
print concat('com.AjouterParametre("',@clef,'",',@clef,');')
print 'return (int)_connexion.ExecuterNonRequete(com)>0;'
print '}'

print concat('public bool Desactiver(',@cleftype,' ',@clef,'){')
print concat('Commande com = new Commande("',@nomtable,'_desact",true);')
print concat('com.AjouterParametre("',@clef,'",',@clef,');')
print 'return (int)_connexion.ExecuterNonRequete(com)>0;'
print '}'


print concat('public IEnumerable<',@nomtable,'> Donner(){')
print concat('Commande com = new Commande(CONST_',upper(@nomtable),'_REQ);')
print concat('return _connexion.ExecuterLecteur(com, x => x.Vers',@nomtable,'());')
--throw new NotImplementedException();
print '}'

print concat('public',' ',@nomtable,' ','Donner(int',' ',@clef,'){')
print concat('Commande com = new Commande($"{CONST_',upper(@nomtable),'_REQ} where ',@clef,'=@',@clef,'");')
print concat('com.AjouterParametre("',@clef,'",',@clef,');')
print concat('return _connexion.ExecuterLecteur(com, x => x.Vers',@nomtable,'()).SingleOrDefault();')
--throw new NotImplementedException();
print '}'

           /* com.AjouterParametre("id", e.id, true);
            com.AjouterParametre("nom", e.nom);
            com.AjouterParametre("description", e.description);

            _connexion.ExecuterNonRequete(com);

            return (int)com.Parametres["id"].Valeur;
            
            throw new NotImplementedException();
        }*/

/*
print concat('public IList<',@nomtable,'> Donner(IList<',@cleftype,'> l, string[] options = null){')
print concat('Commande com = new Commande($"{CONST_',upper(@nomtable),'_REQ} where actif = 1");')
print concat('IList<',@nomtable,'> ll = _connexion.ExecuterLecteur(com, j => j.Vers',@nomtable,'()).ToList();')
print concat('foreach(',@cleftype,' ','i in l){')
print concat('if ( ll.Where(j => j.',@clef,'==i) == null){')
print concat(@nomtable,'Repository rrr = new ',@nomtable,'Repository(/*_dbpf*/);')
print 'll.Add(rrr.Donner(i));}'
print concat('ll.OrderBy(j => j.',@clef,');}')
print 'return ll;}'
*/
print concat('public IEnumerable<',@nomtable,'> Donner(IEnumerable<int> ie, string[] options = null){')
print concat('string requete = $"{CONST_',upper(@nomtable),'_REQ} where actif = 1";')
print 'string clause = "";'
print 'int c = 0;'
print 'Dictionary<string, int> dp = new Dictionary<string, int>();'
print 'foreach(int i in ie){'
print 'c++;'
print 'clause += clause == "" ? "" : " or ";'
print concat('clause += "',@clef,' = @i{c}";')
print 'dp.Add($"i{c}", i);}'
print 'if (c > 0) clause = $"or ({clause})";'
print 'Commande com = new Commande($"{requete} {clause}");'
print 'foreach(KeyValuePair<string,int> k in dp){'
print 'com.AjouterParametre(k.Key, k.Key);}'
print concat('return _connexion.ExecuterLecteur(com, j => j.Vers',@nomtable,'());')
print 'throw new NotImplementedException();}'


print concat('public',' ','int? DonnerParNom(string nom){')
print 'if (nom==null) return null;'
print concat('Commande com = new Commande($"select nom from ({CONST_',upper(@nomtable),'_REQ} where nom = @nom)");')
print 'com.AjouterParametre("nom", nom);'
print 'return (int?)_connexion.ExecuterScalaire(com);}'
print '//throw new NotImplementedException();'
	





