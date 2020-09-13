--select * from v_tables

declare @nomtable varchar(250) = 'role'
DECLARE @idcol int
declare @nomcol VARCHAR(250)
declare @typecol VARCHAR(25)
declare @longueurmax int
declare @vlongueurmax varchar(25)
declare @precision int
declare @scale int
declare @estnullable int

declare @actif int;
declare @paramid varchar(8000);
declare @param_cre varchar(8000)
declare @param_mod varchar(8000);
declare @listeparam_cre varchar(8000)
declare @llisteparam_cre varchar(8000)
declare @out varchar(8000);
declare @listevaleurs_cre varchar(8000)
declare @ttype varchar(8000)
declare @wwhere varchar(8000);
declare @sset varchar(8000);


declare cc cursor for 
select nomtable from v_tables 
--where nomtable not in ('Utilisateur','MessageLu','MessageEfface','UtilisateurAbonnement','UtilisateurRole','UtilisateurNouvelle')
where nomtable = 'Utilisateur'
group by nomtable




open cc
fetch cc into @nomtable

while @@FETCH_STATUS = 0
begin

	print concat('/***txable***',@nomtable,'*/')

	DECLARE c CURSOR FOR
    SELECT  idcol, nomcol, typecol, longueurmax, precision, scale, estnullable FROM v_tables
	where nomtable = @nomtable
	order by idcol
	 
	OPEN c
 
	FETCH c INTO @idcol, @nomcol, @typecol, @longueurmax, @precision, @scale, @estnullable
 


	set @param_cre = ''
	set @listeparam_cre = ''
	set @listevaleurs_cre = ''
	set @param_mod = ''
	set @actif = 0;
	set @sset = '';

	WHILE @@FETCH_STATUS = 0
	BEGIN
    
    


		--@param_cre @id int out,@idprojet int, @idutilisateur int,@montant money
		set @ttype = @typecol;
		if @longueurmax = -1
			set @vlongueurmax = 'MAX'
		else
			set @vlongueurmax = @longueurmax
		if @typecol in( 'varchar','nvarchar')
			set @ttype = concat(@ttype,'(',@vlongueurmax,')')
	
		if @idcol <> 1 and @nomcol <> 'actif'
		begin
			set @param_cre = concat(@param_cre,',')
			set @llisteparam_cre = concat(@llisteparam_cre,',');
		end

		if @nomcol <> 'actif'
		begin
			set @llisteparam_cre = CONCAT(@llisteparam_cre,@nomcol);

			set @param_cre = concat(@param_cre, ' ','@', @nomcol)
			set @param_cre = concat(@param_cre,' ', @ttype);
		end

		if @nomcol = 'actif'
			set @actif = 1;

		if @idcol = 1
			begin
				set @param_cre = concat(@param_cre,' ','out')
				set @out = concat('@',@nomcol)
				set @wwhere = concat('where',' ',@nomcol,'=','@',@nomcol);	
				set @paramid = concat('@',@nomcol,' ',@ttype);
			end

		if @nomcol <> 'actif'
		begin
			if @param_mod <> ''
				set @param_mod = concat(@param_mod,',');
			set @param_mod = concat(@param_mod,'@',@nomcol,' ',@ttype);
			if @idcol > 1
			begin		
			if @idcol <> 2 and @nomcol <> 'actif'
				begin
					set @listeparam_cre = concat(@listeparam_cre,',');
					set @listevaleurs_cre = concat(@listevaleurs_cre,',');
					--set @param_mod = concat(@param_mod,',');
					set @sset = concat(@sset,',');
				end
				set @listeparam_cre = concat(@listeparam_cre,@nomcol);
				set @listevaleurs_cre = concat(@listevaleurs_cre,concat('@',@nomcol));
			
				set @sset = concat(@sset,@nomcol,'=','@',@nomcol);
			end
		end
	

		FETCH c INTO  @idcol, @nomcol, @typecol, @longueurmax, @precision, @scale, @estnullable
	
	END
 
	CLOSE c
	DEALLOCATE c


	if @listeparam_cre <> ''
			set @listeparam_cre = concat('(',@listeparam_cre,')');
	if @listevaleurs_cre <> ''
			set @listevaleurs_cre = concat('(',@listevaleurs_cre,')');

	set @sset = concat('set',' ',@sset);

	print 'go	'
	print concat('drop procedure',' ',@nomtable,'_cre');
	print 'go';
	print concat('create PROCEDURE ' , @nomtable , '_cre')
	print @param_cre
	print 'AS'
	print concat('insert into ', @nomtable, ' ' , @listeparam_cre , ' values ' , @listevaleurs_cre , ';')
	print concat('set ' , @out , ' = @@IDENTITY',';')
	print 'go'

	print concat('drop procedure',' ',@nomtable,'_mod');
	print 'go';
	print concat('create PROCEDURE ' , @nomtable , '_mod')
	print @param_mod
	print 'AS'
	print concat('update',' ', @nomtable)
	print @sset
	print @wwhere
	print ';'
	print 'go'

	print concat('drop procedure',' ',@nomtable,'_eff');
	print 'go';
	print concat('create procedure ',@nomtable, '_eff')
	print @paramid
	print 'AS'
	print concat('delete',' ',@nomtable,' ')
	print @wwhere
	print ';'
	print 'go'
	

	if @actif = 1
	begin
		print concat('drop procedure',' ',@nomtable,'_act');
		print 'go';
		print concat('create procedure',' ',@nomtable,'_act');
		print @paramid;
		print 'AS';
		print concat('update',' ',@nomtable,' ','set actif = 1');
		print @wwhere;
		print ';'
		print 'go';

		print concat('drop procedure',' ',@nomtable,'_desact');
		print 'go';
		print concat('create procedure',' ',@nomtable,'_desact');
		print @paramid;
		print 'AS';
		print concat('update',' ',@nomtable,' ','set actif = 0');
		print @wwhere;
		print ';'
		print 'go';

		print '/*'
		print concat('select',' ',@llisteparam_cre,' ','from',' ',@nomtable )
		print '*/'
	end

	print '/*'
	print concat('private const string CONST_',upper(@nomtable),'_REQ = "','select',' ',@llisteparam_cre,' ','from',' ',@nomtable,'";' )
	print '*/'

	fetch cc into @nomtable
end
close cc
deallocate cc

GO
	

GO


/*
declare @id int
exec utilisateur_cre @id out, 'SUPERADMIN','/','/','loukoumkoum@gmail.com',null,0,null,'1','1','1'


select id,login,nom,prenom,email,datedenaissance,homme,cartedepayement, '***' motdepasse, '***' presel, '***' postsel from Utilisateur*/