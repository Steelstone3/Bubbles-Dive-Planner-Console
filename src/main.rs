use crate::models::dive_profile::DiveProfile;
use crate::models::dive_step::DiveStep;
use crate::presenters::dive_plan::execute_dive_plan::execute_dive_plan;
use crate::presenters::dive_plan::load_dive_plan::load_from_default_file;
use crate::presenters::dive_plan::new_dive_plan::new_dive_plan;
use crate::presenters::dive_plan::update_files::update_files;

mod presenters;
mod models;
mod controllers;
mod commands;
mod factories;

#[cfg(debug_assertions)]
mod tests;

fn main() {
    let (mut dive_profiles, mut dive_steps) = load_from_default_file();
    let (dive_model, mut cylinders) = new_dive_plan();

    loop {
        let dive_plan_data: (DiveProfile, DiveStep) = execute_dive_plan(dive_model, &mut cylinders);
        update_files(&mut dive_profiles, &mut dive_steps, dive_plan_data);
    }
}