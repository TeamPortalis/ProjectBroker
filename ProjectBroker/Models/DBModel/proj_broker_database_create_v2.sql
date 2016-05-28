DROP SCHEMA IF EXISTS projectbrokerschema CASCADE;
CREATE SCHEMA projectbrokerschema;

CREATE TABLE  IF NOT EXISTS projectbrokerschema.p_person(
	p_ID VARCHAR(10) NOT NULL PRIMARY KEY,
	p_fName VARCHAR(50) NOT NULL,
	p_lName VARCHAR(50) NOT NULL,
	p_email VARCHAR(50) NOT NULL,		-- This is okay
	p_password VARCHAR(50) NOT NULL -- This is a BIG TABOO in DB Engineering, very unsecure way of handling passwords
);

CREATE TABLE  IF NOT EXISTS projectbrokerschema.d_department(
	--  The department table is more or less specific for our school, so this should
	--  be changed at a later point
	d_ID VARCHAR(10) NOT NULL PRIMARY KEY,
	d_name VARCHAR(50) NOT NULL
);
CREATE TABLE  IF NOT EXISTS projectbrokerschema.phs_projhostingenv(
	phs_ID VARCHAR(10) NOT NULL PRIMARY KEY ,
	phs_name VARCHAR(50) NOT NULL
);
CREATE TABLE  IF NOT EXISTS projectbrokerschema.pms_projmanagementenv(
	pms_ID VARCHAR(10) NOT NULL PRIMARY KEY ,
	pms_name VARCHAR(50)
);
CREATE TABLE  IF NOT EXISTS projectbrokerschema.s_student(
	s_nr VARCHAR(10) NOT NULL PRIMARY KEY ,
	s_address VARCHAR(50) NOT NULL ,
	s_dob DATE NOT NULL ,
	s_phoneNr CHAR(15) NOT NULL,
	s_d_department VARCHAR(10) NOT NULL , -- same problem with deparment, should be changed later.
	FOREIGN KEY (s_nr) REFERENCES projectbrokerschema.p_person (p_ID) ON UPDATE  CASCADE  ON DELETE CASCADE,
	FOREIGN KEY (s_d_department) REFERENCES projectbrokerschema.d_department(d_id) ON UPDATE CASCADE ON DELETE CASCADE
);
CREATE TABLE  IF NOT EXISTS projectbrokerschema.t_teacher(
	t_ID VARCHAR(10) NOT NULL PRIMARY KEY ,
	FOREIGN KEY (t_ID) REFERENCES projectbrokerschema.p_person (p_ID) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS projectbrokerschema.tm_team(
	tm_ID VARCHAR(10) NOT NULL PRIMARY KEY ,
	tm_name VARCHAR(75) NOT NULL
);

CREATE TABLE IF NOT EXISTS projectbrokerschema.stm_student_in_team(
	stm_s_ID VARCHAR(10) NOT NULL  ,
	stm_tm_ID VARCHAR(10) NOT NULL  ,
	PRIMARY KEY (stm_tm_ID, stm_s_ID),
	FOREIGN KEY (stm_s_ID) REFERENCES projectbrokerschema.s_student(s_nr) ON UPDATE CASCADE ON DELETE CASCADE ,
	FOREIGN KEY (stm_tm_ID) REFERENCES projectbrokerschema.tm_team(tm_ID) ON UPDATE CASCADE ON DELETE CASCADE
);
CREATE TABLE  IF NOT EXISTS projectbrokerschema.pr_project(
	pr_ID VARCHAR(10) NOT NULL PRIMARY KEY ,
	pr_name VARCHAR(50) NOT NULL ,
	pr_wiki VARCHAR(500) NOT NULL ,
	pr_image VARCHAR(500) NULL, -- added image for project site since it is necessary.
	pr_t_ID VARCHAR(10) NOT NULL ,
	pr_pms_ID VARCHAR(10) NOT NULL ,
	pr_phs_ID VARCHAR(10) NOT NULL ,
	pr_tm_ID VARCHAR(10) NOT NULL ,

	FOREIGN KEY  (pr_t_ID) REFERENCES  projectbrokerschema.t_teacher(t_id) ON DELETE CASCADE ON UPDATE CASCADE ,
	FOREIGN KEY (pr_pms_ID) REFERENCES projectbrokerschema.pms_projmanagementenv(pms_id) ON DELETE CASCADE ON UPDATE CASCADE ,
	FOREIGN KEY (pr_phs_ID) REFERENCES projectbrokerschema.phs_projhostingenv(phs_id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (pr_tm_ID) REFERENCES projectbrokerschema.tm_team(tm_id) ON DELETE CASCADE ON UPDATE CASCADE
);


-- Here is the secure way of doing password

CREATE TABLE IF NOT EXISTS projectbrokerschema.l_login_info (
	l_id SERIAL NOT NULL,
	l_authtoken VARCHAR(62) NOT NULL ,
	l_salt VARCHAR(31) NOT NULL,
	PRIMARY KEY (l_id)
);

CREATE TABLE IF NOT EXISTS projectbrokerschema.lpr_login_person_relation (
	lpr_l_login  INT          NOT NULL,
	lpr_p_person VARCHAR(10)  NOT NULL,
	lpr_username VARCHAR(300) NOT NULL,
	PRIMARY KEY (lpr_l_login, lpr_p_person),
	FOREIGN KEY (lpr_l_login) REFERENCES projectbrokerschema.l_login_info (l_id) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (lpr_p_person) REFERENCES projectbrokerschema.p_person (p_ID) ON UPDATE CASCADE ON DELETE CASCADE
);


DROP FUNCTION IF EXISTS projectbrokerschema.uf_create_net_user(
	IN username VARCHAR(100),
	IN password VARCHAR(100)
);
CREATE OR REPLACE FUNCTION projectbrokerschema.uf_create_new_user(
	IN username VARCHAR(100),
	IN password VARCHAR(100),
	IN persId 	VARCHAR(10)
) RETURNS VOID AS $$
DECLARE
	numOfElements INT := 0;
	hashedString VARCHAR(61);
	salt VARCHAR(61);
	lid INT;
BEGIN
	numOfElements := (SELECT count(*) FROM projectbrokerschema.lpr_login_person_relation AS pl
	WHERE username = pl.lpr_username);

	IF numOfElements > 0
	THEN
		RETURN;
	END IF;

	salt := (SELECT gen_salt('bf', 8));

	hashedString := (
		SELECT crypt("password", salt)
	);

	INSERT INTO projectbrokerschema.l_login_info (l_authtoken, l_salt)
		VALUES (hashedString, salt);

	lid := (SELECT l.l_id FROM projectbrokerschema.l_login_info AS l
		WHERE l.l_authtoken = hashedString AND l.l_salt = salt);

	INSERT INTO projectbrokerschema.lpr_login_person_relation (lpr_l_login, lpr_p_person, lpr_username)
		VALUES (lid, persId, username);
END;
$$ LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS projectbrokerschema.uf_authenticate(
	IN username VARCHAR(100),
	IN password VARCHAR(100),
 IN cursorName VARCHAR(100)
);
CREATE OR REPLACE FUNCTION projectbrokerschema.uf_authenticate(
	IN username VARCHAR(100),
	IN password VARCHAR(100)
) RETURNS TABLE(
	pid VARCHAR(10),
	authenticated BOOLEAN
) AS $$
DECLARE
	lid INT;
	pid VARCHAR(10);
	hashedPass VARCHAR(100);
	salt VARCHAR(100);
	intRowCount INT;
BEGIN
	DROP TABLE IF EXISTS  res1;
	CREATE TEMPORARY TABLE res1 ON COMMIT DROP AS SELECT rel.lpr_l_login AS lid, rel.lpr_p_person AS pid  FROM projectbrokerschema.lpr_login_person_relation AS rel LIMIT 1;
	lid := (SELECT res.lid FROM res1 AS res LIMIT 1);
	pid := (SELECT res.pid FROM res1 AS res LIMIT 1);

	salt := (SELECT l.l_salt FROM projectbrokerschema.l_login_info AS l WHERE L.l_id = lid LIMIT 1);

	hashedPass := (
		SELECT crypt(password, salt)
	);

	intRowCount := (SELECT count(*) FROM projectbrokerschema.l_login_info AS l WHERE l.l_id = lid AND l.l_authtoken = hashedPass);

	DROP TABLE res1;

	IF intRowCount > 0 THEN
		RETURN QUERY SELECT pid, true;
	ELSE
		RETURN QUERY SELECT pid, false;
	END IF;

END;
$$ LANGUAGE plpgsql;
