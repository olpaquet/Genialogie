use Genealogie
--select * from v_tables

declare @yes nvarchar(max);
declare @ttype varchar(100);

declare @nomtable varchar(250) = 'Role'
DECLARE @idcol int
declare @nomcol VARCHAR(250)
declare @typecol VARCHAR(25)
declare @longueurmax int
declare @precision int
declare @scale int
declare @estnullable int

declare cc cursor for 
select distinct nomtable from v_tables;


 

 open cc 
 fetch cc into @nomtable
 while @@FETCH_STATUS = 0
 begin

	--print @nomtable

	DECLARE c CURSOR FOR
    SELECT  idcol, nomcol, typecol, longueurmax, precision, scale, estnullable FROM v_tables
	where nomtable = @nomtable
	order by idcol
	OPEN c
 
	FETCH c INTO @idcol, @nomcol, @typecol, @longueurmax, @precision, @scale, @estnullable
 

	set @yes = '';

	WHILE @@FETCH_STATUS = 0
	BEGIN


		select @ttype = CASE
			WHEN @typecol in('nvarchar', 'varchar') THEN 'string'  
			when @typecol = 'money' then 'decimal'
			when @typecol = 'datetime' then 'DateTime'  
			ELSE @typecol
		end

		if @estnullable = 1 and @ttype in ('int','DateTime','decimal')
			set @ttype = concat(@ttype,'?');


		if @yes <> ''
			set @yes = CONCAT(@yes,char(10));
		set @yes = concat(@yes,'public',' ', @ttype,' ',@nomcol,' {get; set;}');

		FETCH c INTO  @idcol, @nomcol, @typecol, @longueurmax, @precision, @scale, @estnullable
	
	END
 
	CLOSE c
	DEALLOCATE c
	print concat('class',' ',@nomtable,'');
	print '{';
	print @yes;        
			/*public int actif { get; set; }
			public int id { get; set; }
			public string nom { get; set; }
			public string description { get; set; }

			public Categorie() { }*/
	print '}';
	
	fetch cc into @nomtable

end
close cc
deallocate cc






GO
	

GO



