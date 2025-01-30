CREATE TABLE [SCHEMA].t_client_di_release(
	id_release varchar(32) NOT NULL,
	id_version varchar(64) NOT NULL,
	no_release_name varchar(256) NOT NULL,
	tx_path_origem varchar(512) NOT NULL,
	tx_path_destiny varchar(512) NOT NULL,
	tx_description varchar(1024) NOT NULL,
	id_release_type varchar(32) NOT NULL,
	no_manager varchar(64) NOT NULL,
	tx_execution_log varchar(max) NOT NULL,
	id_status varchar(32) NOT NULL,
	tx_job_tracker varchar(max) NOT NULL,

	dt_modify datetime NOT NULL,
	id_modify varchar(32) NOT NULL,

	CONSTRAINT pk_release PRIMARY KEY CLUSTERED 
	(
		id_release ASC
	)
)

ALTER TABLE [SCHEMA].t_client_di_release ADD  DEFAULT (getdate()) FOR [dt_modify]

ALTER TABLE [SCHEMA].t_client_di_release ADD  DEFAULT ('INTEFLOW') FOR [id_modify]