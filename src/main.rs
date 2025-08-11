use crate::{controllers::file::FileController, models::dive_stage::DiveStage};

mod commands;
mod controllers;
mod models;
mod presenters;

fn main() {
    println!("Welcome to Bubbles Dive Planner Console Rust");

    let mut file = FileController::default();
    let mut dive_stages: Vec<DiveStage> = Vec::new();

    let mut dive_stage = file.load_dive_plan();
    let cylinders = file.load_cylinders();

    loop {
        dive_stage = dive_stage.update(cylinders.to_owned());
        dive_stage.print_result();

        dive_stages.push(dive_stage);

        if file.upsert_dive_stage(&dive_stages).is_err() {
            println!("Failed to create dive profile file")
        };
    }
}
