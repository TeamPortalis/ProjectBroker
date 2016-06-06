INSERT INTO projectbrokerschema.d_department (d_id, d_name)
    VALUES
      ('D000000000', 'Test Department 1.'),
      ('D000000001', 'Test Department 2.'),
      ('D000000002', 'Test Department 3.'),
      ('D000000003', 'Test Department 4.');


INSERT INTO projectbrokerschema.p_person (p_id, p_fname, p_lname, p_email)
    VALUES
      ('0000000000', 'Alexnader', 'Mandt', 'testmail@mail.com'),
      ('23221AB333', 'Michal', 'McCree', 'testmail2@mail.com'),
      ('4ACF932ANF', 'Andra', 'Whatever', 'testmail3@mail.com'),
      ('LEGAL22222', 'Whynot', 'Wieso', 'testmail4@mail.com'),
      ('FORTHEWIN1', 'Lala', 'Wieso', 'testmail5@mail.com'),
      ('WHYNOT1234', 'Nadiene', 'Westerwald', 'testmail6@mail.com'),
      ('THE0000001', 'Moritz', 'Brandstaetter', 'testy@testmail.test');

INSERT INTO projectbrokerschema.t_teacher (t_id)
    VALUES
      ('23221AB333'),
      ('WHYNOT1234'),
      ('THE0000001');




INSERT INTO projectbrokerschema.s_student (s_nr, s_address, s_dob, s_d_department)
    VALUES
      ('0000000000', 'Addresse 1', '1998-11-22', 'D000000000'),
      ('4ACF932ANF', 'Addresse 2', '1998-11-21', 'D000000001'),
      ('LEGAL22222', 'Addresse 3', '1998-11-20', 'D000000002'),
      ('FORTHEWIN1', 'Addresse 4', '1998-11-19', 'D000000003');


SELECT projectbrokerschema.uf_create_new_user('user1', '1234', '0000000000');
SELECT projectbrokerschema.uf_create_new_user('user2', '5678', '23221AB333');
SELECT projectbrokerschema.uf_create_new_user('user3', 'user3', '4ACF932ANF');
SELECT projectbrokerschema.uf_create_new_user('user4', 'user4', 'LEGAL22222');
SELECT projectbrokerschema.uf_create_new_user('user5', 'user5', 'FORTHEWIN1');
SELECT projectbrokerschema.uf_create_new_user('user6', 'user6', 'WHYNOT1234');
SELECT projectbrokerschema.uf_create_new_user('brandy', 'killerpwd', 'THE0000001');

INSERT INTO projectbrokerschema.phs_projhostingenv(phs_id, phs_name)
    VALUES
      ('PHV1', 'TestEnv 1.');

INSERT INTO projectbrokerschema.pms_projmanagementenv (pms_id, pms_name)
    VALUES
      ('PMS1', 'Test Manage 1.');

INSERT INTO projectbrokerschema.tm_team (tm_id, tm_name) VALUES
  ('TM1', 'Team 1'),
  ('TM2', 'Team 2'),
  ('TM3', 'Team 3');

INSERT INTO projectbrokerschema.stm_student_in_team (stm_s_id, stm_tm_id) VALUES
  ('0000000000', 'TM1'),
  ('4ACF932ANF', 'TM2'),
  ('LEGAL22222', 'TM3'),
  ('FORTHEWIN1', 'TM3');

INSERT INTO projectbrokerschema.pr_project (pr_id, pr_name, pr_desc, pr_image, pr_t_id, pr_pms_id, pr_phs_id, pr_tm_id)
    VALUES
      ('PID1', 'Project Broker', 'Wiki for simple things', NULL, '23221AB333', 'PMS1', 'PHV1', 'TM1'),
      ('PID2', 'Project Broker 2', 'Wiki for simple things 2', NULL, '23221AB333', 'PMS1', 'PHV1', 'TM2'),
      ('PID3', 'Project Broker 3', 'Wiki for simple things 3', NULL, '23221AB333', 'PMS1', 'PHV1', 'TM3');